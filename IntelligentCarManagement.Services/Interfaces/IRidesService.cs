using Models;
using Models.Data_Transfer_Objects;
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
        Task<IEnumerable<RideDTO>> GetAllAsync();
        Task<IEnumerable<RideDTO>> GetClientsAllAsync(int clientId);
        Task<IEnumerable<RideDTO>> GetDriversAllAsync(int driverId);
        Task<RideDTO> GetOngoingAsync(int driverId);
        Task ConfirmRequestAsync(int rideId);
        Task EndAsync(int rideId);
        Task<RideDTO> GetAsync(int id);
        Task RemoveAsync(int id);
        Task<RequestResponse> RequestAsync(RideDTO ride);
        void Update(int id, Ride ride);
        Task AssignDriverAsync(int rideId, int driverId);
        Task RateAsync(int rideId, double rating);
        Task EvaluateAccuracy(int rideId, double accuracy);
    }
}
