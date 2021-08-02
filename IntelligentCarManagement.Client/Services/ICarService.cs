using IntelligentCarManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Client.Services
{
    public interface ICarService
    {
        Task<IEnumerable<Car>> GetCars();
        Task<Driver> GetCarDriver(int carId);
    }
}
