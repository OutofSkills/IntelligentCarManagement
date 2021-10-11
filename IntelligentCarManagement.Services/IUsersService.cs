using IntelligentCarManagement.Models;
using IntelligentCarManagement.Models.NotMapped_Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Services
{
    public interface IUsersService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserAsync(int id);
        Task RemoveUserAsync(int userId);
        Task UpdateUserRoles(User user);
        void EditUser(User user);
        Task RegisterUser(RegisterModel model);
        Task ChangePasswordAsync(ResetPasswordModel model);
        Task<IEnumerable<string>> GetUserRolesAsync(int userId);
    }
}
