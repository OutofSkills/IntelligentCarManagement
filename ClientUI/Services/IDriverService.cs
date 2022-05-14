using ClientUI.Utils;
using Models.DTOs;

namespace ClientUI.Services
{
    public interface IDriverService
    {
        Task<RequestResponse> SubmitApplication(DriverApplicationDTO model);
        Task<IEnumerable<DriverApplicationDTO>> GetApplicationsAsync();
        Task<DriverApplicationDTO> GetApplicationAsync(int id);
    }
}
