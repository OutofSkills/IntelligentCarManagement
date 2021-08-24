using IntelligentCarManagement.Api.Helpers;
using IntelligentCarManagement.Models;
using IntelligentCarManagement.Models.NotMapped_Models;
using IntelligentCarManagement.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var collection = await usersService.GetAllUsersAsync();

            HttpContext.InsertPaginationParameterInResponse(collection, pagination.NumberOfRecords);

            return Ok(collection.Paginate(pagination).ToList());
        }

        [HttpGet]
        [Route("get-user")]
        public async Task<IActionResult> GetUserAsync([FromQuery] int userId)
        {
            var user = await usersService.GetUserAsync(userId);

            return Ok(user);
        }

        [HttpPost]
        [Route("edit")]
        public IActionResult UpdateUser([FromBody]User user)
        {
            var result = usersService.EditUser(user);

            if(result is true)
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("edit-roles")]
        public async Task<IActionResult> UpdateUserRolesAsync([FromBody] User user)
        {
            var result = await usersService.UpdateUserRoles(user);

            if (result is true)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]User user)
        {
            var result = await usersService.RegisterUser(user);

            if (result.ToLower() == "success")
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost]
        [Route("remove-account")]
        public async Task<IActionResult> RemoveAccountAsync([FromForm] int userId)
        {
            var result = await usersService.RemoveUserAsync(userId);

            if (result is true)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("change-password")]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] ResetPasswordModel resetPasswordModel)
        {
            var result = await usersService.ChangePasswordAsync(resetPasswordModel); 

            if (result is true)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("get-user-roles")]
        public async Task<IActionResult> GetUserRolesAsync([FromQuery] int userId)
        {
            return Ok(await usersService.GetUserRolesAsync(userId));
        }
    }
}
