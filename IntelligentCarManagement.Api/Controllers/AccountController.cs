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
        private readonly ITokenService tokenService;
        private readonly IUsersService usersService;

        public AccountController(ITokenService tokenService, IUsersService usersService)
        {
            this.tokenService = tokenService;
            this.usersService = usersService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            if(await tokenService.IsValidUsernameAndPassword(loginModel.Email, loginModel.Password))
            {
                return Ok(await tokenService.GenerateToken(loginModel.Email));
            }
            else
            {
                return BadRequest();
            }    
        }

        //[HttpDelete]
        //[Route("remove-account")]
        //public async Task<IActionResult> RemoveAccountAsync([FromForm] int id)
        //{
        //    try
        //    {
        //        await usersService.RemoveUserAsync(id);
        //    }
        //    catch (Exception e)
        //    {
        //        var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
        //        {
        //            Content = new StringContent(e.Message),
        //            ReasonPhrase = "Remove user error"

        //        };

        //        throw new System.Web.Http.HttpResponseException(response);
        //    }

        //    return Ok();
        //}

        //[HttpPost]
        //[Route("change-password")]
        //public async Task<IActionResult> ChangePasswordAsync([FromBody] ResetPasswordModel resetPasswordModel)
        //{
        //    try
        //    {
        //        await usersService.ChangePasswordAsync(resetPasswordModel);
        //    }
        //    catch (Exception e)
        //    {
        //        var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
        //        {
        //            Content = new StringContent(e.Message),
        //            ReasonPhrase = "Reset password error"

        //        };

        //        throw new System.Web.Http.HttpResponseException(response);
        //    }

        //    return Ok();
        //}
    }
}
