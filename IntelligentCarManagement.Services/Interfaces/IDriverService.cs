using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Services
{
    public interface IDriverService
    {
        Task<Driver> GetDriver(int id);
        Driver GetCarDriver(int carID);
        void AddDriver(Driver driver);
        void UpdateDriver(Driver driver);
        Task<IEnumerable<Driver>> GetDrivers();
        Task ChangeDriverStatusAsync(int driverId, string statusName);
    }
}
