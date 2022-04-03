using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Client.Services
{
    public interface IRidesService
    {
        Task<string> SheduleNewRideAsync(Ride ride);
        Task<Ride> FindRideAsync(int id);
    }
}
