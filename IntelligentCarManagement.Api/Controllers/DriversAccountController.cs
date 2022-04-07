using Api.Services.Interfaces;
using IntelligentCarManagement.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Data_Transfer_Objects;
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
    public class DriversAccountController : Controller
    {
        private readonly IDriversAccountService accountService;

        public DriversAccountController(IDriversAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<LoginResponse> Login([FromBody] LoginModel loginModel)
        {
            var token = await accountService.Login(loginModel);

            return new LoginResponse() { Token = token};
        }

        [HttpPost]
        [Route("register")]
        public async Task Register([FromBody] DriverRegisterModel model)
        {
            try
            {
                await accountService.Register(model);
            }
            catch (Exception ex)
            {

            }
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
