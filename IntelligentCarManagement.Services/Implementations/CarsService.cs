using AutoMapper;
using IntelligentCarManagement.DataAccess.UnitsOfWork;
using Models;
using Models.Data_Transfer_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Services
{
    public class CarsService : ICarsService
    {
        private readonly IUnitOfWork unitOfWork;

        public CarsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void AssignDriver(int carId, string driverEmail)
        {
            var driver = unitOfWork.DriversRepo.GetByEmail(driverEmail);

            if(driver is null)
            {
                return;
            }

            driver.CarId = carId;

            unitOfWork.DriversRepo.Update(driver);
            unitOfWork.SaveChanges();
        }

        public void Create(CarDTO dto)
        {
            var car = new Car();

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<CarDTO, Car>();
            });

            IMapper iMapper = config.CreateMapper();
            car = iMapper.Map<CarDTO, Car>(dto);

            unitOfWork.CarsRepo.Insert(car);
            unitOfWork.SaveChanges();
        }

        public Task EditAsync(int id, CarDTO car)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CarDTO>> GetAllAsync()
        {
            var cars = await unitOfWork.CarsRepo.GetAll();

            // Mapper config
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Car, CarDTO>();
            });

            var result = new List<CarDTO>();
            foreach (var car in cars)
            {
                IMapper iMapper = config.CreateMapper();
                var dto = iMapper.Map<Car, CarDTO>(car);

                result.Add(dto);
            }

            return result;
        }

        public Task<CarDTO> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(int carId)
        {
            throw new NotImplementedException();
        }
    }
}
