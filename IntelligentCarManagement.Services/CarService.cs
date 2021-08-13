using IntelligentCarManagement.DataAccess.UnitsOfWork;
using IntelligentCarManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Services
{
    public class CarService : ICarService
    {
        private readonly IUnitOfWork _repository;

        public CarService(IUnitOfWork repository)
        {
            _repository = repository;
        }

        public bool AddCar(Car car)
        {
            throw new NotImplementedException();
        }

        public bool EditCar(Car car)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Car>> GetAllCars()
        {
            return await _repository.CarsRepo.GetAll();
        }

        public Car GetCar(int id)
        {
            throw new NotImplementedException();
        }

        public bool RemoveCar(Car car)
        {
            throw new NotImplementedException();
        }
    }
}
