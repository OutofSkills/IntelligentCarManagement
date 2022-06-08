using IntelligentCarManagement.Api.Helpers;
using IntelligentCarManagement.Api.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Data_Transfer_Objects;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Api.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesService rolesService;

        public RolesController(IRolesService rolesService)
        {
            this.rolesService = rolesService;
        }

        [HttpGet]
        public async Task<IEnumerable<RoleDTO>> GetAllAsync()
        {
            return await rolesService.GetAllRolesAsync();
        }

        [HttpGet]
        [Route("id")]
        public async Task<RoleDTO> GetRoleAsync([FromQuery]int id)
        {
            return await rolesService.GetRoleAsync(id);
        }

        [HttpDelete]
        [Route("id")]
        public async Task<bool> RemoveRoleAsync([FromQuery] int id)
        {
            return await rolesService.RemoveRoleAsync(id);
        }

        [HttpPut]
        [Route("id")]
        public async Task<bool> EditRoleAsync([FromQuery] int id, [FromBody] RoleDTO role)
        {
            return await rolesService.EditRoleAsync(id, role);
        }

        [HttpPost]
        public async Task<bool> AddRoleAsync([FromBody] RoleDTO role)
        {
            return await rolesService.AddRoleAsync(role);
        }
    }
}
