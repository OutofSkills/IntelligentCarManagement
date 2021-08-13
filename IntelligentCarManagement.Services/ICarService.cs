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
        Car GetCar(int id);
        bool RemoveCar(Car car);
        bool AddCar(Car car);
        bool EditCar(Car car);
    }
}
