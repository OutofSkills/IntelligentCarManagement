using IntelligentCarManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Services
{
    public interface ICarService
    {
        Task<IEnumerable<Car>> GetAllCars();
        Task<Car> GetCarAsync(int id);
        Task RemoveCarAsync(int carId);
        void AddCar(Car car);
        void EditCar(Car car);
    }
}
