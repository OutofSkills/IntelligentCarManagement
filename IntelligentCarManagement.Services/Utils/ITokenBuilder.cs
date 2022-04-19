using Api.Services.Utils;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Services
{
    public interface ITokenBuilder
    {
        Task<string> GenerateToken(string username);
    }
}