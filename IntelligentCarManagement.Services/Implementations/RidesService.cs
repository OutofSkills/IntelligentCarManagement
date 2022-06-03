using Api.Services.CustomExceptions;
using Api.Services.Utils;
using AutoMapper;
using IntelligentCarManagement.DataAccess.UnitsOfWork;
using IntelligentCarManagement.Services;
using Models;
using Models.Data_Transfer_Objects;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Api.Services
{
    public class RidesService : IRidesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationService notificationService;

        public RidesService(IUnitOfWork unitOfWork, INotificationService notificationService)
        {
            this._unitOfWork = unitOfWork;
            this.notificationService = notificationService;
        }

        public async Task<RequestResponse> RequestAsync(RideDTO ride)
        {
            if (ride is null)
                throw new Exception("No data provided");

            if (ride.PickUpTime == default)
                ride.PickUpTime = DateTime.Now;

            // Check if the client exists
            var client = await _unitOfWork.ClientsRepo.GetById(ride.ClientId);
            if (client is null)
                throw new Exception("Client not found");

            // Check if the driver is available
            var driver = await _unitOfWork.DriversRepo.GetById(ride.DriverId);
            if (driver is null)
                throw new Exception("Driver not found");

            if (!driver.IsAvailable)
                return new RequestResponse() { Success = false, Message = $"Driver {driver.FirstName} {driver.LastName} is not available." };

            /* If the driver exists and is available
             * change his availability to not available 
             */
            driver.IsAvailable = false;
            _unitOfWork.DriversRepo.Update(driver);

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<RideDTO, Ride>();
                cfg.CreateMap<RideStateDTO, RideState>();
                cfg.AddGlobalIgnore("Id");
                cfg.AddGlobalIgnore("Review");
            });

            IMapper iMapper = config.CreateMapper();

            var newRide = iMapper.Map<RideDTO, Ride>(ride);

            var states = await _unitOfWork.RideStatesRepo.GetAll();
            newRide.RideState = states.Where(s => s.Name == "ASSIGNED").FirstOrDefault();

            _unitOfWork.RidesRepo.Insert(newRide);
            _unitOfWork.SaveChanges();

            // Notify the picked driver
            return await NotifyDriverAsync(driver, client);
        }

        private async Task<RequestResponse> NotifyDriverAsync(Driver driver, Client client)
        {
            var notificationCategory = await notificationService.GetNotificationCategoryAsync(NotificationCategories.NEW_RIDE);
            var notificationResponse = await notificationService.SendAsync(driver.Id, new NotificationDTO
            {
                Title = "New available ride",
                Body = $"User {client.UserName} requested a new ride.",
                NotificationCategory = notificationCategory
            });

            if (!notificationResponse.Success)
                return new RequestResponse()
                {
                    Success = false,
                    Message = "Couldn't notify our driver about the incoming request. Please contact him via one of his contacts"
                };

            return new RequestResponse { Success = true, Message = $"Driver {driver.FirstName} {driver.LastName} was successfully notified about your request." };
        }

        public async Task AssignDriverAsync(int rideId, int driverId)
        {
            var ride = await _unitOfWork.RidesRepo.GetById(rideId);
            if (ride is null)
                throw new Exception("Couldn't find information about the ride with the given id");

            var driver = await _unitOfWork.DriversRepo.GetById(driverId);
            if (driver is null)
                throw new Exception("Couldn't find information about the driver with the given id");

            ride.DriverId = driverId;
            _unitOfWork.RidesRepo.Update(ride);
            _unitOfWork.SaveChanges();

        }

        public void Update(int id, Ride ride)
        {
            _unitOfWork.RidesRepo.Update(ride);
            _unitOfWork.SaveChanges();
        }

        public async Task ConfirmRequestAsync(int rideId)
        {
            var ride = await _unitOfWork.RidesRepo.GetById(rideId);
            var newState = await _unitOfWork.RideStatesRepo.GetAll();

            if (ride is null)
                throw new NotFoundException($"Can't find a ride with id {rideId}");

            ride.RideStateId = newState.Where(s => s.Name == "STARTED").FirstOrDefault().Id;

            _unitOfWork.RidesRepo.Update(ride);
            _unitOfWork.SaveChanges();
        }

        public async Task EndAsync(int rideId)
        {
            var ride = await _unitOfWork.RidesRepo.GetById(rideId);
            var newState = await _unitOfWork.RideStatesRepo.GetAll();

            if (ride is null)
                throw new NotFoundException($"Can't find a ride with id {rideId}");

            ride.RideStateId = newState.Where(s => s.Name == "FINISHED").FirstOrDefault().Id;

            _unitOfWork.RidesRepo.Update(ride);
            _unitOfWork.SaveChanges();
        }

        public async Task<IEnumerable<RideDTO>> GetAllAsync()
        {
            var rides = await _unitOfWork.RidesRepo.GetAll();

            var result = new List<RideDTO>();

            // Map the driver model to the driver DTO
            // Map view model to user model
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Ride, RideDTO>();
                cfg.CreateMap<Client, ClientDTO>();
                cfg.CreateMap<Driver, DriverDTO>();
                cfg.CreateMap<RideState, RideStateDTO>();
                cfg.CreateMap<Review, ReviewDTO>();
                cfg.CreateMap<Car, CarDTO>();

            });

            IMapper iMapper = config.CreateMapper();

            foreach (var ride in rides)
            {
                var dto = iMapper.Map<Ride, RideDTO>(ride);
                // Get client's rating
                var clientRating = ride.Client.DriverReviews.Sum(r => r.Rating) / ride.Client.DriverReviews.Count;
                dto.Client.Rating = Math.Round(clientRating, 1);
                // Get driver's rating
                double? driverRating = GetDriverRating(rides, ride.DriverId);
                dto.Driver.Rating = (float)Math.Round((double)driverRating, 1);

                // Get accuracy
                double? driverAccuracy = DriversService.GetDriverAccuracy(rides, ride.Driver.Id);
                dto.Driver.Accuracy = Math.Round((double)driverAccuracy, 1);

                result.Add(dto);
            }

            return result;
        }

        public async Task<RideDTO> GetAsync(int id)
        {
            var ride =  await _unitOfWork.RidesRepo.GetById(id);

            if (ride is null)
                return null;

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Ride, RideDTO>();
                cfg.CreateMap<Client, ClientDTO>();
                cfg.CreateMap<Driver, DriverDTO>();
                cfg.CreateMap<RideState, RideStateDTO>();
                cfg.CreateMap<Review, ReviewDTO>();
                cfg.CreateMap<Car, CarDTO>();
            });

            IMapper iMapper = config.CreateMapper();
            var rideDto = iMapper.Map<Ride, RideDTO>(ride);
            rideDto.Client.Avatar = FileCompressor.Decompress(rideDto.Client.Avatar);
            rideDto.Driver.Avatar = FileCompressor.Decompress(rideDto.Driver.Avatar);

            // Get client's rating
            var rating = ride.Client.DriverReviews.Sum(r => r.Rating) / ride.Client.DriverReviews.Count;
            rideDto.Client.Rating = Math.Round(rating, 1);

            // Get driver's rating
            var rides = await _unitOfWork.RidesRepo.GetAll();
            double? driverRating = GetDriverRating(rides, ride.DriverId);
            rideDto.Driver.Rating = (float)Math.Round((double)driverRating, 1);

            // Get accuracy
            double? driverAccuracy = DriversService.GetDriverAccuracy(rides, ride.Driver.Id);
            rideDto.Driver.Accuracy = Math.Round((double)driverAccuracy, 1);

            return rideDto;
        }

        public async Task<RideDTO> GetOngoingAsync(int driverId)
        {
            var rides = await _unitOfWork.RidesRepo.GetAll();
            var ongoingRide = rides.Where(ride => ride.DriverId == driverId && (ride.RideState.Name == "STARTED" || ride.RideState.Name == "ASSIGNED")).FirstOrDefault();

            if (ongoingRide is null)
                return null;

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Ride, RideDTO>();
                cfg.CreateMap<Client, ClientDTO>();
                cfg.CreateMap<Driver, DriverDTO>();
                cfg.CreateMap<RideState, RideStateDTO>();
                cfg.CreateMap<Review, ReviewDTO>();
                cfg.CreateMap<Car, CarDTO>();
            });

            IMapper iMapper = config.CreateMapper();
            var rideDto = iMapper.Map<Ride, RideDTO>(ongoingRide);
            if (rideDto != null)
            {
                rideDto.Client.Avatar = FileCompressor.Decompress(rideDto.Client.Avatar);
                rideDto.Driver.Avatar = FileCompressor.Decompress(rideDto.Driver.Avatar);

                // Get client's rating
                var rating = ongoingRide.Client.DriverReviews.Sum(r => r.Rating) / ongoingRide.Client.DriverReviews.Count;
                rideDto.Client.Rating = Math.Round(rating, 1);

                // Get driver's rating
                double? driverRating = GetDriverRating(rides, ongoingRide.DriverId);
                rideDto.Driver.Rating = (float)Math.Round((double)driverRating, 1);

                // Get accuracy
                double? driverAccuracy = DriversService.GetDriverAccuracy(rides, ongoingRide.Driver.Id);
                rideDto.Driver.Accuracy = Math.Round((double)driverAccuracy, 1);
            }

            return rideDto;
        }

        public async Task RemoveAsync(int id)
        {
            await _unitOfWork.RidesRepo.Delete(id);
            _unitOfWork.SaveChanges();
        }

        public async Task<IEnumerable<RideDTO>> GetClientsAllAsync(int clientId)
        {
            var rides = await _unitOfWork.RidesRepo.GetAll();
            rides = rides.Where(ride => ride.ClientId == clientId);

            var result = new List<RideDTO>();

            // Map the driver model to the driver DTO
            // Map view model to user model
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Ride, RideDTO>();
                cfg.CreateMap<Client, ClientDTO>();
                cfg.CreateMap<Driver, DriverDTO>();
                cfg.CreateMap<RideState, RideStateDTO>();
                cfg.CreateMap<Review, ReviewDTO>();
                cfg.CreateMap<Car, CarDTO>();
            });

            IMapper iMapper = config.CreateMapper();

            foreach (var ride in rides)
            {
                var rideObj = iMapper.Map<Ride, RideDTO>(ride);
                rideObj.Client.Avatar = FileCompressor.Decompress(rideObj.Client.Avatar);
                rideObj.Driver.Avatar = FileCompressor.Decompress(ride.Driver.Avatar);

                // Get client's rating
                var rating = ride.Client.DriverReviews.Sum(r => r.Rating) / ride.Client.DriverReviews.Count;
                rideObj.Client.Rating = Math.Round(rating, 1);

                // Get driver's rating
                double? driverRating = GetDriverRating(rides, ride.DriverId);
                rideObj.Driver.Rating = (float)Math.Round((double)driverRating, 1);

                // Get accuracy
                double? driverAccuracy = DriversService.GetDriverAccuracy(rides, ride.Driver.Id);
                rideObj.Driver.Accuracy = Math.Round((double)driverAccuracy, 1);

                result.Add(rideObj);
            }

            return result;
        }

        public async Task<IEnumerable<RideDTO>> GetDriversAllAsync(int driverId)
        {
            var rides = await _unitOfWork.RidesRepo.GetAll();
            rides = rides.Where(ride => ride.DriverId == driverId);

            var result = new List<RideDTO>();

            // Map the driver model to the driver DTO
            // Map view model to user model
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Ride, RideDTO>();
                cfg.CreateMap<Client, ClientDTO>();
                cfg.CreateMap<Driver, DriverDTO>();
                cfg.CreateMap<RideState, RideStateDTO>();
                cfg.CreateMap<Review, ReviewDTO>();
                cfg.CreateMap<Car, CarDTO>();
            });

            IMapper iMapper = config.CreateMapper();

            foreach (var ride in rides)
            {
                var rideObj = iMapper.Map<Ride, RideDTO>(ride);
                rideObj.Client.Avatar = FileCompressor.Decompress(rideObj.Client.Avatar);
                rideObj.Driver.Avatar = FileCompressor.Decompress(ride.Driver.Avatar);

                // Get client's rating
                var rating = ride.Client.DriverReviews.Sum(r => r.Rating) / ride.Client.DriverReviews.Count;
                rideObj.Client.Rating = Math.Round(rating, 1);

                // Get driver's rating
                double? driverRating = GetDriverRating(rides, ride.DriverId);
                rideObj.Driver.Rating = (float)Math.Round((double)driverRating, 1);

                // Get accuracy
                double? driverAccuracy = DriversService.GetDriverAccuracy(rides, ride.Driver.Id);
                rideObj.Driver.Accuracy = Math.Round((double)driverAccuracy, 1);

                result.Add(rideObj);
            }

            return result;
        }

        public static double? GetDriverRating(IEnumerable<Ride> rides, int driverId)
        {
            var ratedRides = rides.Where(r => r.Review is not null && r.Review.Rating != null && r.DriverId == driverId).ToList();
            var driverRating = ratedRides.Sum(r => r.Review.Rating) / ratedRides.Count;
            return driverRating;
        }

        public async Task RateAsync(int rideId, double rating)
        {
            var ride = await _unitOfWork.RidesRepo.GetById(rideId);

            if(ride is null)
            {
                throw new Exception($"A ride with the id {rideId} doesn't exist.");
            }

            if (ride.Review is null)
            {
                var review = new Review()
                {
                    Rating = Math.Round(rating, 1)
                };

                _unitOfWork.ReviewsRepo.Insert(review);
                _unitOfWork.SaveChanges();

                ride.RideReviewId = review.Id;
                _unitOfWork.RidesRepo.Update(ride);
            }
            else
            {
                ride.Review.Rating = Math.Round(rating, 1); 
                _unitOfWork.ReviewsRepo.Update(ride.Review);
            }
            
            _unitOfWork.SaveChanges();
        }

        public async Task EvaluateAccuracy(int rideId, double accuracy)
        {
            var ride = await _unitOfWork.RidesRepo.GetById(rideId);

            if (ride is null)
            {
                throw new Exception($"A ride with the id {rideId} doesn't exist.");
            }

            if (ride.Review is null)
            {
                var review = new Review()
                {
                    DrivingAccuracy = Math.Round(accuracy, 1)
                };

                _unitOfWork.ReviewsRepo.Insert(review);
                _unitOfWork.SaveChanges();

                ride.RideReviewId = review.Id;
                _unitOfWork.RidesRepo.Update(ride);
            }
            else
            {
                ride.Review.DrivingAccuracy = Math.Round(accuracy, 1);
                _unitOfWork.ReviewsRepo.Update(ride.Review);
            }

            _unitOfWork.SaveChanges();
        }
    }
}
