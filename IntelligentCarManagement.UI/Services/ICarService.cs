using IntelligentCarManagement.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentCarManagement.UI.Services
{
    public interface ICarService
    {
        Task<IEnumerable<Car>> GetCars();
    }
}
