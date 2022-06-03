using Models;
using Models.Data_Transfer_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Services
{
    public interface ICarsService
    {
        Task<IEnumerable<CarDTO>> GetAllAsync();
        Task<CarDTO> GetAsync(int id);
        Task RemoveAsync(int carId);
        void Create(CarDTO car);
        Task EditAsync(int id, CarDTO car);
        void AssignDriver(int carId, string driverEmail);
    }
}
