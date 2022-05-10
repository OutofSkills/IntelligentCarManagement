using Models;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Api.Services
{
    public interface IRidesService
    {
        Task<IEnumerable<Ride>> GetAllAsync();
        Task<IEnumerable<Ride>> GetAllAsync(int clientId);
        Task<Ride> GetAsync(int id);
        Task RemoveAsync(int id);
        Task<RideRequestResponse> RequestAsync(Ride ride);
        void Update(int id, Ride ride);
        Task AssignDriverAsync(int rideId, int driverId);
    }
}
