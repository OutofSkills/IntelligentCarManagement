using IntelligentCarManagement.Models;
using IntelligentCarManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetUsers()
        {
            return Ok(usersService.GetAllUsers());
        }

        [HttpGet]
        [Route("UpdateUser")]
        public IActionResult UpdateUser(User user)
        {
            return Ok(usersService.EditUser(user));
        }
    }
}
