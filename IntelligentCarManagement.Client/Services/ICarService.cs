using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Client.Services
{
    public interface ICarService
    {
        Task<HttpResponseMessage> GetCarsAsync(int page = 1, int recordsPerPage = 10);
        Task<Driver> GetCarDriver(int carId);
        Task<bool> RemoveCarAsync(int carId);
        Task<bool> AddNewCar(Car car);
        Task<bool> EditCarAsync(Car car);
    }
}
