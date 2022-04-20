using Api.Services.Utils;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Services
{
    public interface ITokenBuilder
    {
        Task<string> BuildAsync(string username);
    }
}