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
        Task<bool> RemoveCarAsync(int carId);
        bool AddCar(Car car);
        bool EditCar(Car car);
    }
}
