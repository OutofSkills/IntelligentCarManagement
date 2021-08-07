using IntelligentCarManagement.Models;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Client.Services
{
    public interface IAuthenticationService
    {
        Task<User> Login(User userForAtuthetication);
        Task Logout();
    }
}