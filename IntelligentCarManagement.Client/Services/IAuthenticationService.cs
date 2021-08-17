using IntelligentCarManagement.Models;
using IntelligentCarManagement.Models.NotMapped_Models;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Client.Services
{
    public interface IAuthenticationService
    {
        Task<string> Register(User user);
        Task<bool> ChangePasswordAsync(ResetPasswordModel model);
        Task<User> Login(LoginModel userForAtuthetication);
        Task Logout();
    }
}