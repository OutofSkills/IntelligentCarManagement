using IntelligentCarManagement.Api.Helpers;
using IntelligentCarManagement.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Api.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [HttpGet]
        [Route("get-users")]
        public async Task<IActionResult> GetUsersAsync([FromQuery]Pagination pagination)
        {
            var collection = await usersService.GetAllAsync();

            HttpContext.InsertPaginationParameterInResponse(collection, pagination.NumberOfRecords);

            return Ok(collection.Paginate(pagination).ToList());
        }

        [HttpGet]
        [Route("byEmail")]
        public async Task<UserBaseDTO> GetUserAsync([FromQuery] string email)
        {
            var user = await usersService.GetAsync(email);

            return user;
        }

        [HttpGet]
        [Route("byId")]
        public async Task<UserBaseDTO> GetUserAsync([FromQuery] int userId)
        {
            var user = await usersService.GetAsync(userId);

            return user;
        }

        [HttpPut]
        public async Task<UserBaseDTO> UpdatAsync([FromQuery] int id, [FromBody] UserBaseDTO user)
        {
            return await usersService.EditAsync(id, user);
        }
    }
}
