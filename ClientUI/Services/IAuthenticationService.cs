using ClientUI.Utils;
using Models.DTOs;
using System.Threading.Tasks;

namespace ClientUI.Services
{
    public interface IAuthenticationService
    {
        Task<Utils.RequestResponse> Register(ClientRegisterModel model);
        Task<Utils.RequestResponse> ChangePasswordAsync(ResetPasswordDTO model);
        Task<Utils.RequestResponse> Login(string email, string password);
        Task Logout();
    }
}