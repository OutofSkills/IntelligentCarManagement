using IntelligentCarManagement.Models;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Client.Services
{
    public interface IAuthenticationService
    {
        Task<string> Register(User user);
        Task<User> Login(LoginModel userForAtuthetication);
        Task Logout();
    }
}