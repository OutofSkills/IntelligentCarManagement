using IntelligentCarManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Api.Services
{
    public interface IDriverService
    {
        Driver GetDriver(int id);
        Driver GetCarDriver(int carID);
    }
}
