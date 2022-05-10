using ClientUI.Utils;
using Models.DTOs;
using System.Threading.Tasks;

namespace ClientUI.Services
{
    public interface IAuthenticationService
    {
        Task<RequestResponse> Register(ClientRegisterModel model);
        Task<RequestResponse> ChangePasswordAsync(ResetPasswordDTO model);
        Task<RequestResponse> Login(string email, string password);
        Task Logout();
    }
}