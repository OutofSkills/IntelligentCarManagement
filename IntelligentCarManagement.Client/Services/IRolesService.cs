using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Client.Services
{
    public interface IRolesService
    {
        Task<HttpResponseMessage> GetRolesAsync(int page = 1, int recordsPerPage = 10);
        Task<IEnumerable<Role>> GetRolesAsync();
        Task<bool> RemoveRoleAsync(int id);
        Task<bool> EditRoleAsync(Role role);
        Task<bool> AddRoleAsync(Role newRole);
    }
}
