using IntelligentCarManagement.Api.Helpers;
using IntelligentCarManagement.Api.Services;
using IntelligentCarManagement.Models;
using IntelligentCarManagement.Models.NotMapped_Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [Route("get-all-roles")]
        public async Task<IActionResult> GetRolesAsync()
        {
            return Ok(await rolesService.GetAllRolesAsync());
        }

        [HttpGet]
        [Route("get-roles")]
        public async Task<IActionResult> GetRolesAsync([FromQuery] Pagination pagination)
        {
            var collection = await rolesService.GetAllRolesAsync();

            HttpContext.InsertPaginationParameterInResponse(collection, pagination.NumberOfRecords);

            return Ok(collection.Paginate(pagination).ToList());
        }

        [HttpGet]
        [Route("get-role")]
        public async Task<IActionResult> GetRoleAsync([FromQuery]int id)
        {
            return Ok(await rolesService.GetRoleAsync(id));
        }

        [HttpGet]
        [Route("remove-role")]
        public async Task<IActionResult> RemoveRoleAsync([FromQuery] int id)
        {
            return Ok(await rolesService.RemoveRoleAsync(id));
        }

        [HttpPost]
        [Route("edit-role")]
        public async Task<IActionResult> EditRoleAsync([FromBody] Role role)
        {
            return Ok(await rolesService.EditRoleAsync(role));
        }

        [HttpPost]
        [Route("add-role")]
        public async Task<IActionResult> AddRoleAsync([FromBody] Role role)
        {
            return Ok(await rolesService.AddRoleAsync(role));
        }
    }
}
