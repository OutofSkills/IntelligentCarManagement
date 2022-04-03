using Api.Services.Interfaces;
using IntelligentCarManagement.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Api.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<string> Login([FromBody] LoginModel loginModel)
        {
            var token = await accountService.Login(loginModel);

            return token;
        }

        [HttpPost]
        [Route("register")]
        public async Task Register([FromBody] RegisterModel model)
        {
            await accountService.Register(model);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task RemoveAccountAsync([FromQuery] int id)
        {
            await accountService.Remove(id);
        }

        [HttpPost]
        [Route("password")]
        public async Task ChangePasswordAsync([FromBody] ResetPasswordModel resetPasswordModel)
        {
           await accountService.ChangePassword(resetPasswordModel);
        }
    }
}
