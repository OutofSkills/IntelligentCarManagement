using Models;
using Models.Data_Transfer_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Api.Services
{
    public interface IRolesService
    {
        Task<IEnumerable<RoleDTO>> GetAllRolesAsync();
        Task<RoleDTO> GetRoleAsync(int id);
        Task<bool> RemoveRoleAsync(int id);
        Task<bool> EditRoleAsync(int id, RoleDTO role);
        Task<bool> AddRoleAsync(RoleDTO role);
    }
}
