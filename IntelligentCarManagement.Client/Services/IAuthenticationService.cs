using Models.DTOs;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Client.Services
{
    public interface IAuthenticationService
    {
        Task<string> Register(ClientRegisterModel model);
        Task<bool> ChangePasswordAsync(ResetPasswordDTO model);
        Task Login(LoginModel userForAtuthetication);
        Task Logout();
    }
}