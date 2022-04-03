using IntelligentCarManagement.DataAccess.UnitsOfWork;
using Models;
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

        public void AddCar(Car car)
        {
            unitOfWork.CarsRepo.Insert(car);
            unitOfWork.SaveChanges();
        }

        public void EditCar(Car car)
        {
            unitOfWork.CarsRepo.Update(car);
            unitOfWork.SaveChanges();
        }

        public async Task<IEnumerable<Car>> GetAllCars()
        {
            return await unitOfWork.CarsRepo.GetAll();
        }

        public async Task<Car> GetCarAsync(int id)
        {
            return await unitOfWork.CarsRepo.GetById(id);
        }

        public async Task RemoveCarAsync(int carId)
        {
            await unitOfWork.CarsRepo.Delete(carId);
            unitOfWork.SaveChanges();
        }
    }
}
