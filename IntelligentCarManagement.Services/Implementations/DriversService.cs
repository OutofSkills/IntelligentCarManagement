using Api.Services.CustomExceptions;
using Api.Services.Utils;
using AutoMapper;
using IntelligentCarManagement.Api.Services;
using IntelligentCarManagement.DataAccess.UnitsOfWork;
using Microsoft.AspNetCore.Identity;
using Models;
using Models.Data_Transfer_Objects;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Services
{
    public class DriversService : IDriversService
    {
        private readonly IUnitOfWork unitOfWork;

        public DriversService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public DriverDTO Add(DriverDTO driver)
        {
            throw new NotImplementedException();
        }

        public async Task<DriverDTO> GetAsync(int id)
        {
            var driver = await unitOfWork.DriversRepo.GetById(id);

            // Map the driver model to the driver DTO
            // Map view model to user model
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Driver, DriverDTO>();
                cfg.CreateMap<Car, CarDTO>();
            });

            IMapper iMapper = config.CreateMapper();
            var dto = iMapper.Map<Driver, DriverDTO>(driver);

            dto.Avatar = FileCompressor.Decompress(dto.Avatar);

            // Get rating
            double? driverRating = RidesService.GetDriverRating(driver.Rides, driver.Id);
            dto.Rating = (float)Math.Round((double)driverRating, 1);

            // Get accuracy
            double? driverAccuracy = GetDriverAccuracy(driver.Rides, driver.Id);
            dto.Accuracy = Math.Round((double)driverAccuracy, 1);

            return dto;
        }

        public DriverDTO Get(string email)
        {
            var driver = unitOfWork.DriversRepo.GetByEmail(email);

            // Map the driver model to the driver DTO
            // Map view model to user model
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Driver, DriverDTO>();
                cfg.CreateMap<Car, CarDTO>();
            });

            IMapper iMapper = config.CreateMapper();
            var dto = iMapper.Map<Driver, DriverDTO>(driver);

            dto.Avatar = FileCompressor.Decompress(dto.Avatar);
            // Get rating
            double? driverRating = RidesService.GetDriverRating(driver.Rides, driver.Id);
            dto.Rating = (float)Math.Round((double)driverRating, 1);

            // Get accuracy
            double? driverAccuracy = GetDriverAccuracy(driver.Rides, driver.Id);
            dto.Accuracy = Math.Round((double)driverAccuracy, 1);

            return dto;
        }

        public async Task<IEnumerable<DriverDTO>> GetAllAsync(bool? availability)
        {
            var drivers = await unitOfWork.DriversRepo.GetAll();

            var result = new List<DriverDTO>();

            // Map the driver model to the driver DTO
            // Map view model to user model
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Driver, DriverDTO>();
                cfg.CreateMap<Car, CarDTO>();
            });

            IMapper iMapper = config.CreateMapper();

            if (availability is not null)
            {
                foreach (var driver in drivers)
                {
                    if (driver.IsAvailable == availability)
                    {
                        var dto = iMapper.Map<Driver, DriverDTO>(driver);
                        dto.Avatar = FileCompressor.Decompress(dto.Avatar);

                        // Get rating
                        double? driverRating = RidesService.GetDriverRating(driver.Rides, driver.Id);
                        dto.Rating = (float)Math.Round((double)driverRating, 1);

                        // Get accuracy
                        double? driverAccuracy = GetDriverAccuracy(driver.Rides, driver.Id);
                        dto.Accuracy = Math.Round((double)driverAccuracy, 1);

                        result.Add(dto);
                    }
                }
            }
            else
            {
                foreach (var driver in drivers)
                {
                    var dto = iMapper.Map<Driver, DriverDTO>(driver);
                    dto.Avatar = FileCompressor.Decompress(dto.Avatar);

                    // Get rating
                    double? driverRating = RidesService.GetDriverRating(driver.Rides, driver.Id);
                    dto.Rating = (float)Math.Round((double)driverRating, 1);

                    // Get accuracy
                    double? driverAccuracy = GetDriverAccuracy(driver.Rides, driver.Id);
                    dto.Accuracy = Math.Round((double)driverAccuracy, 1);

                    result.Add(dto);
                }
            }

            return result;
        }

        public async Task<DriverDTO> UpdateAsync(int id, DriverDTO driverDTO)
        {
            Driver driver = await unitOfWork.DriversRepo.GetById(id);
            if (driver == null)
                throw new NotFoundException("No driver found with the given id.");

            // Map the driver model to the driver DTO
            var config = new MapperConfiguration(cfg => {
                cfg.AddGlobalIgnore("id");
                cfg.CreateMap<DriverDTO, Driver>();
                cfg.CreateMap<Car, CarDTO>();
            });

            IMapper iMapper = config.CreateMapper();
            driver = iMapper.Map(driverDTO, driver);

            // Compress data
            driver.Avatar = FileCompressor.Compress(driver.Avatar);

            unitOfWork.DriversRepo.Update(driver);
            unitOfWork.SaveChanges();

            return driverDTO;
        }

        public async Task BecomeAvailable(int driverId, bool availability)
        {
            var driver = await unitOfWork.DriversRepo.GetById(driverId);

            if (driver is null)
                throw new NotFoundException("Couldn't find a driver with the provided id.");

            driver.IsAvailable = availability;
            unitOfWork.DriversRepo.Update(driver);
            unitOfWork.SaveChanges();
        }

        public async Task RateClientAsync(int rideId, double rating)
        {
            var ride = await unitOfWork.RidesRepo.GetById(rideId);

            if(ride is null)
            {
                throw new NotFoundException($"The ride with id {rideId} wasn't found.");
            }

            var review = new ClientReview()
            {
                ClientId = ride.ClientId,
                Rating = rating,
                DriverId = ride.DriverId,
            };

            unitOfWork.ClientReviewsRepo.Insert(review);
            unitOfWork.SaveChanges();
        }

        public async Task UpdateLocationAsync(int driverId, string latitude, string longitude)
        {
            var driver = await unitOfWork.DriversRepo.GetById(driverId);

            if (driver is null)
                throw new NotFoundException($"Driver with id {driverId} not found.");

            driver.CurrentLat = latitude;
            driver.CurrentLong = longitude;

            unitOfWork.DriversRepo.Update(driver);
            unitOfWork.SaveChanges();
        }

        public static double? GetDriverAccuracy(IEnumerable<Ride> rides, int driverId)
        {
            var ratedRides = rides.Where(r => r.Review is not null && r.Review.DrivingAccuracy != null && r.DriverId == driverId).ToList();
            var driverAccuracy = ratedRides.Sum(r => r.Review.DrivingAccuracy) / ratedRides.Count;
            return driverAccuracy;
        }

        public async Task<bool> IsAvailable(int driverId)
        {
            var driver = await unitOfWork.DriversRepo.GetById(driverId);

            if(driver is null)
            {
                throw new NotFoundException($"Driver with id {driverId} wasn't found.");
            }

            return driver.IsAvailable;
        }
    }
}
