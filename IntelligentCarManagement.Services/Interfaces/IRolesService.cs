using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Api.Services
{
    public interface IRolesService
    {
        Task<IEnumerable<Role>> GetAllRolesAsync();
        Task<Role> GetRoleAsync(int id);
        Task<bool> RemoveRoleAsync(int id);
        Task<bool> EditRoleAsync(Role role);
        Task<bool> AddRoleAsync(Role role);
    }
}
