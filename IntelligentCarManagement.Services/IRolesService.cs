using IntelligentCarManagement.Models;
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
    }
}
