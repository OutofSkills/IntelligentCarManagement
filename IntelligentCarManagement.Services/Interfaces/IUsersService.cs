using Models;
using Models.View_Models;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Services
{
    public interface IUsersService
    {
        Task<IEnumerable<UserBase>> GetAllUsersAsync();
        Task<UserBase> GetUserAsync(int id);
        Task RemoveUserAsync(int userId);
        Task UpdateUserRoles(UserBase user);
        void EditUser(UserBase user);
        Task RegisterUser(RegisterModel model);
        Task ChangePasswordAsync(ResetPasswordModel model);
        Task<IEnumerable<string>> GetUserRolesAsync(int userId);
    }
}
