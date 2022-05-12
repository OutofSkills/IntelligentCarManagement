using ClientUI.Utils;
using Models.DTOs;

namespace ClientUI.Services
{
    public interface IDriverService
    {
        Task<RequestResponse> SubmitDriverApplication(DriverApplicationDTO model);
    }
}
