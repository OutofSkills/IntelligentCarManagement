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

        public async Task<bool> AddRoleAsync(Role role)
        {
            var result = await roleManager.CreateAsync(role);

            return result.Succeeded;
        }

        public async Task<bool> EditRoleAsync(Role role)
        {
            var roleToUpdate = await roleManager.FindByIdAsync(role.Id.ToString());

            roleToUpdate.Name = role.Name;
            roleToUpdate.Description = role.Description;

            var result = await roleManager.UpdateAsync(roleToUpdate);

            return result.Succeeded;
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await roleManager.Roles.ToListAsync(); 
        }

        public async Task<Role> GetRoleAsync(int id)
        {
            var role = await roleManager.FindByIdAsync(id.ToString());

            return role;
        }

        public async Task<bool> RemoveRoleAsync(int id)
        {
            var role = await roleManager.FindByIdAsync(id.ToString());
            var result = await roleManager.DeleteAsync(role);

            return result.Succeeded;
        }
    }
}
