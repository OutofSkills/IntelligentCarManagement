using Api.Services.Interfaces;
using IntelligentCarManagement.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    public class ClientsAccountController : Controller
    {
        private readonly IClientsAccountService accountService;

        public ClientsAccountController(IClientsAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<LoginResponse> Login([FromBody] LoginModel loginModel)
        {
            var loginResponse = await accountService.Login(loginModel);

            return loginResponse;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ClientRegisterModel> Register([FromBody] ClientRegisterModel model)
        {
            await accountService.Register(model);

            return model;
        }

        [HttpDelete]
        [Route("delete")]
        public async Task RemoveAccountAsync([FromQuery] int id)
        {
            await accountService.Remove(id);
        }

        [HttpPost]
        [Route("password")]
        public async Task ChangePasswordAsync([FromBody] ResetPasswordDTO resetPasswordModel)
        {
           await accountService.ChangePassword(resetPasswordModel);
        }
    }
}
