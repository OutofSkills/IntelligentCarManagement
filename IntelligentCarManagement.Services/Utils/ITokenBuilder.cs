using System.Threading.Tasks;

namespace IntelligentCarManagement.Services
{
    public interface ITokenBuilder
    {
        Task<dynamic> GenerateToken(string username);
        Task<bool> IsValidUsernameAndPassword(string username, string password);
    }
}