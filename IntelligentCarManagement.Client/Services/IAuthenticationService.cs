using Models.View_Models;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Client.Services
{
    public interface IAuthenticationService
    {
        Task<string> Register(RegisterModel model);
        Task<bool> ChangePasswordAsync(ResetPasswordModel model);
        Task Login(LoginModel userForAtuthetication);
        Task Logout();
    }
}