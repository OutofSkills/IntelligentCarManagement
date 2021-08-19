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
        Task<bool> RemoveUserAsync(int userId);
        bool AddUser(User user);
        Task<bool> UpdateUserRoles(User user);
        bool EditUser(User user);
        Task<string> RegisterUser(User user);
        Task<bool> ChangePasswordAsync(ResetPasswordModel model);
        Task<IEnumerable<string>> GetUserRolesAsync(int userId);
    }
}
