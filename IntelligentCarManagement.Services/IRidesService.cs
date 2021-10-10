using IntelligentCarManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Api.Services
{
    public interface IRidesService
    {
        Task<IEnumerable<Ride>> GetAllRides();
        Task<Ride> GetRideAsync(int id);
        Task RemoveRideAsync(int id);
        int AddRide(Ride ride);
        void EditRide(Ride ride);
        Task AssignDriverToRideAsync(int rideId, int driverId);
    }
}
