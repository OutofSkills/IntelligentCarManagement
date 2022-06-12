using ClientUI.Utils;
using Models;

namespace ClientUI.Services.Statuses
{
    public interface IStatusesService
    {
        Task<IEnumerable<AccountStatus>> GetAccountStatusesAsync();
        Task<RequestResponse> AddAccountStatusAsync(AccountStatus model);
        Task<IEnumerable<ApplicationStatus>> GetApplicationStatusesAsync();
        Task<RequestResponse> AddApplicationStatusAsync(ApplicationStatus model);
        Task<IEnumerable<RideState>> GetRideStatusesAsync();
        Task<RequestResponse> AddRideStatusAsync(RideState model);
    }
}
