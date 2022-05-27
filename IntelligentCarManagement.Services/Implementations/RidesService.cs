using Api.Services.CustomExceptions;
using Api.Services.Utils;
using AutoMapper;
using IntelligentCarManagement.DataAccess.UnitsOfWork;
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
            });

            IMapper iMapper = config.CreateMapper();

            var newRide = iMapper.Map<RideDTO, Ride>(ride);

            var states = await _unitOfWork.RideStatesRepo.GetAll();
            newRide.RideState = states.Where(s => s.Name == "PROCESSING").FirstOrDefault();

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
                NotificaionCategory = notificationCategory
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

            ride.RideStateId = newState.Where(s => s.Name == "ONGOING").FirstOrDefault().Id;

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
                cfg.CreateMap<RideStateDTO, RideState>();
            });

            IMapper iMapper = config.CreateMapper();

            foreach (var ride in rides)
            {
                result.Add(iMapper.Map<Ride, RideDTO>(ride));
            }

            return result;
        }

        public async Task<RideDTO> GetAsync(int id)
        {
            var ride =  await _unitOfWork.RidesRepo.GetById(id);

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Ride, RideDTO>();
                cfg.CreateMap<Client, ClientDTO>();
                cfg.CreateMap<Driver, DriverDTO>();
                cfg.CreateMap<RideState, RideStateDTO>();
            });

            IMapper iMapper = config.CreateMapper();
            var rideDto = iMapper.Map<Ride, RideDTO>(ride);
            rideDto.Client.Avatar = FileCompressor.Decompress(rideDto.Client.Avatar);
            rideDto.Driver.Avatar = FileCompressor.Decompress(rideDto.Driver.Avatar);

            return rideDto;
        }

        public async Task<RideDTO> GetOngoingAsync(int driverId)
        {
            var rides = await _unitOfWork.RidesRepo.GetAll();
            var ongoingRide = rides.Where(ride => ride.DriverId == driverId && ride.RideState.Name == "ONGOING").FirstOrDefault();

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Ride, RideDTO>();
                cfg.CreateMap<Client, ClientDTO>();
                cfg.CreateMap<Driver, DriverDTO>();
                cfg.CreateMap<RideState, RideStateDTO>();
            });

            IMapper iMapper = config.CreateMapper();
            var rideDto = iMapper.Map<Ride, RideDTO>(ongoingRide);
            if (rideDto != null)
            {
                rideDto.Client.Avatar = FileCompressor.Decompress(rideDto.Client.Avatar);
                rideDto.Driver.Avatar = FileCompressor.Decompress(rideDto.Driver.Avatar);
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
            });

            IMapper iMapper = config.CreateMapper();

            foreach (var ride in rides)
            {
                var rideObj = iMapper.Map<Ride, RideDTO>(ride);
                rideObj.Client.Avatar = FileCompressor.Decompress(rideObj.Client.Avatar);
                rideObj.Driver.Avatar = FileCompressor.Decompress(ride.Driver.Avatar);
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
            });

            IMapper iMapper = config.CreateMapper();

            foreach (var ride in rides)
            {
                var rideObj = iMapper.Map<Ride, RideDTO>(ride);
                rideObj.Client.Avatar = FileCompressor.Decompress(rideObj.Client.Avatar);
                rideObj.Driver.Avatar = FileCompressor.Decompress(ride.Driver.Avatar);
                result.Add(rideObj);
            }

            return result;
        }
    }
}
