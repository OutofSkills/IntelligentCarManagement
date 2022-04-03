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

        [HttpPut]
        [Route("edit")]
        public IActionResult UpdateUser([FromBody]UserBase user)
        {
            try
            {
                usersService.EditUser(user);
            }
            catch (Exception e)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(e.Message),
                    ReasonPhrase = "Edit user error"

                };

                throw new System.Web.Http.HttpResponseException(response);
            }

            return Ok();
        }

        [HttpPost]
        [Route("edit-roles")]
        public async Task<IActionResult> UpdateUserRolesAsync([FromBody] UserBase user)
        {
            try
            {
                await usersService.UpdateUserRoles(user);
            }
            catch (Exception e)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(e.Message),
                    ReasonPhrase = "Edit user error"

                };

                throw new System.Web.Http.HttpResponseException(response);
            }

            return Ok();
        }

        [HttpGet]
        [Route("get-user-roles")]
        public async Task<IActionResult> GetUserRolesAsync([FromQuery] int userId)
        {
            return Ok(await usersService.GetUserRolesAsync(userId));
        }
    }
}
