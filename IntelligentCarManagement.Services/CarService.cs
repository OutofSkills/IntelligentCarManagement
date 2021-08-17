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
        private readonly IUnitOfWork unitOfWork;

        public CarService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public bool AddCar(Car car)
        {
            var success = true;
            try
            {
                unitOfWork.CarsRepo.Insert(car);
                unitOfWork.SaveChanges();
            }
            catch(Exception e)
            {
                //handle exception
                return !success;
            }

            return success;
        }

        public bool EditCar(Car car)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Car>> GetAllCars()
        {
            return await unitOfWork.CarsRepo.GetAll();
        }

        public Car GetCar(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveCarAsync(int carId)
        {
            var success = true;
            try
            {
                await unitOfWork.CarsRepo.Delete(carId);
            }
            catch(Exception e)
            {
                return !success;
            }

            return success;
        }
    }
}
