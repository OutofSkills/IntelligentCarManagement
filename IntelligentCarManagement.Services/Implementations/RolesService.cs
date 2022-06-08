using IntelligentCarManagement.DataAccess.UnitsOfWork;
using Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Data_Transfer_Objects;
using AutoMapper;

namespace IntelligentCarManagement.Api.Services
{
    public class RolesService : IRolesService
    {
        private readonly RoleManager<Role> roleManager;

        public RolesService(RoleManager<Role> roleManager)
        {
            this.roleManager = roleManager;
        }

        public async Task<bool> AddRoleAsync(RoleDTO dto)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<RoleDTO, Role>();
            });

            IMapper iMapper = config.CreateMapper();
            var role = iMapper.Map<RoleDTO, Role>(dto);

            var result = await roleManager.CreateAsync(role);

            return result.Succeeded;
        }

        public async Task<bool> EditRoleAsync(int id, RoleDTO dto)
        {
            var role = await roleManager.FindByIdAsync(id.ToString());
            if (role is null)
                return false;

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<RoleDTO, Role>();
                cfg.AddGlobalIgnore("Id");
            });

            IMapper iMapper = config.CreateMapper();
            var roleToUpdate = iMapper.Map<RoleDTO, Role>(dto, role);

            var result = await roleManager.UpdateAsync(roleToUpdate);
            
            return result.Succeeded;
        }

        public async Task<IEnumerable<RoleDTO>> GetAllRolesAsync()
        {
            var roles = await roleManager.Roles.ToListAsync();

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Role, RoleDTO>();
            });
            IMapper iMapper = config.CreateMapper();

            var result = new List<RoleDTO>();
            foreach(var role in roles)
            {
                var dto = iMapper.Map<Role, RoleDTO>(role);

                result.Add(dto);
            }

            return result; 
        }

        public async Task<RoleDTO> GetRoleAsync(int id)
        {
            var role = await roleManager.FindByIdAsync(id.ToString());

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Role, RoleDTO>();
            });
            IMapper iMapper = config.CreateMapper();
            var dto = iMapper.Map<Role, RoleDTO>(role);

            return dto;
        }

        public async Task<bool> RemoveRoleAsync(int id)
        {
            var role = await roleManager.FindByIdAsync(id.ToString());
            var result = await roleManager.DeleteAsync(role);

            return result.Succeeded;
        }
    }
}
