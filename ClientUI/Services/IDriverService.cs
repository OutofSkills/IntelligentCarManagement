using ClientUI.Utils;
using Models.DTOs;

namespace ClientUI.Services
{
    public interface IDriverService
    {
        Task<Utils.RequestResponse> SubmitApplication(DriverApplicationDTO model);
        Task<IEnumerable<DriverApplicationDTO>> GetApplicationsAsync();
        Task<DriverApplicationDTO> GetApplicationAsync(int id);
        Task<Utils.RequestResponse> ApproveApplicationAsync(int id);
        Task<Utils.RequestResponse> RejectApplicationAsync(int id);
    }
}
