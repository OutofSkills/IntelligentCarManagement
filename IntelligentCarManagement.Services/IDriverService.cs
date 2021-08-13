using IntelligentCarManagement.Models;
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
    }
}
