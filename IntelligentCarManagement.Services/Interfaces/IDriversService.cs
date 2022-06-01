using Models;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Services
{
    public interface IDriversService
    {
        Task<DriverDTO> GetAsync(int id);
        DriverDTO Get(String email);
        DriverDTO Add(DriverDTO driver);
        Task<DriverDTO> UpdateAsync(int id, DriverDTO dto);
        Task<IEnumerable<DriverDTO>> GetAllAsync(bool? availability);
        Task BecomeAvailable(int driverId, bool availability);
        Task RateClientAsync(int rideId, double rating);
    }
}
