using Models;
using Models.View_Models;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Services
{
    public interface IUsersService
    {
        Task<IEnumerable<UserBaseDTO>> GetAllAsync();
        Task<UserBaseDTO> GetAsync(int id);
        Task<UserBaseDTO> GetAsync(string email);
        Task RemoveAsync(int userId);
        //Task UpdateRoles(int userId, string[] roles);
        Task<UserBaseDTO> EditAsync(int id, UserBaseDTO model);
    }
}
