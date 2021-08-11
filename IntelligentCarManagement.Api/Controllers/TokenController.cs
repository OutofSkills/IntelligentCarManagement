using IntelligentCarManagement.Models;
using IntelligentCarManagement.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Api.Controllers
{
    [Route("/token")]
    public class TokenController : Controller
    {
        private readonly ITokenService tokenService;
        private readonly UserManager<User> userManager;

        public TokenController(ITokenService tokenService, UserManager<User> userManager)
        {
            this.tokenService = tokenService;
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromForm] LoginModel loginModel)
        {
            if(await tokenService.IsValidUsernameAndPassword(loginModel.Email, loginModel.Password))
            {
                return new ObjectResult(await tokenService.GenerateToken(loginModel.Email));
            }
            else
            {
                return BadRequest();
            }    
        }
    }
}
