using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Api.Services
{
    public interface IStatusesService
    {
        Task<IEnumerable<AccountStatus>> GetAccountStatusesAsync();
        Task<IEnumerable<ApplicationStatus>> GetApplicationStatusesAsync();
        Task<IEnumerable<RideState>> GetRideStatusesAsync();
        Task RemoveAccountStatusAsync(int id);
        void EditAccountStatus(AccountStatus status);
        void AddAccountStatus(AccountStatus status);
        void AddRideStatus(RideState status);
        void AddApplicationStatus(ApplicationStatus status);
    }
}
