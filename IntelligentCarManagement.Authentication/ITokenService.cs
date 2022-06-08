using System.Threading.Tasks;

namespace Client.Authentication
{
    public interface ITokenService
    {
        Task<string> GetTokenAsync();
    }
}