using IntelligentCarManagement.Api.Helpers;
using IntelligentCarManagement.Models;
using IntelligentCarManagement.Models.NotMapped_Models;
using IntelligentCarManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Api.Controllers
{
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
        [Route("GetUsers")]
        public async Task<IActionResult> GetUsersAsync([FromQuery]Pagination pagination)
        {
            var collection = await usersService.GetAllUsersAsync();

            HttpContext.InsertPaginationParameterInResponse(collection, pagination.NumberOfRecords);

            return Ok(collection.Paginate(pagination).ToList());
        }

        [HttpGet]
        [Route("UpdateUser")]
        public IActionResult UpdateUser(User user)
        {
            return Ok(usersService.EditUser(user));
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
    }
}
