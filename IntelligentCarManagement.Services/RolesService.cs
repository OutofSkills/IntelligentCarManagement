using IntelligentCarManagement.DataAccess.UnitsOfWork;
using IntelligentCarManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Api.Services
{
    public class RolesService : IRolesService
    {
        private readonly RoleManager<Role> roleManager;

        public RolesService(RoleManager<Role> roleManager)
        {
            this.roleManager = roleManager;
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await roleManager.Roles.ToListAsync(); 
        }
    }
}
