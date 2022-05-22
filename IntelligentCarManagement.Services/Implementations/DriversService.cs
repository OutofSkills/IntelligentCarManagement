using Api.Services.CustomExceptions;
using Api.Services.Utils;
using AutoMapper;
using IntelligentCarManagement.DataAccess.UnitsOfWork;
using Microsoft.AspNetCore.Identity;
using Models;
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
            });

            IMapper iMapper = config.CreateMapper();
            var dto = iMapper.Map<Driver, DriverDTO>(driver);

            dto.Avatar = FileCompressor.Decompress(dto.Avatar);

            return dto;
        }

        public DriverDTO Get(String email)
        {
            var driver = unitOfWork.DriversRepo.GetByEmail(email);

            // Map the driver model to the driver DTO
            // Map view model to user model
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Driver, DriverDTO>();
            });

            IMapper iMapper = config.CreateMapper();
            var dto = iMapper.Map<Driver, DriverDTO>(driver);

            dto.Avatar = FileCompressor.Decompress(dto.Avatar);

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
    }
}
