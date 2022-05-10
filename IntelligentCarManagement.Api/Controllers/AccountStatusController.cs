using IntelligentCarManagement.Api.Helpers;
using IntelligentCarManagement.Api.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTOs;
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
    public class AccountStatusController : ControllerBase
    {
        private readonly IAccountStatusService accountStatusService;

        public AccountStatusController(IAccountStatusService accountStatusService)
        {
            this.accountStatusService = accountStatusService;
        }

        [HttpGet]
        [Route("get-all-statuses")]
        public async Task<IActionResult> GetStatusesAsync()
        {
            return Ok(await accountStatusService.GetAllStatusesAsync());
        }

        [HttpGet]
        [Route("get-statuses")]
        public async Task<IActionResult> GetStatusesAsync([FromQuery] Pagination pagination)
        {
            var collection = await accountStatusService.GetAllStatusesAsync();

            HttpContext.InsertPaginationParameterInResponse(collection, pagination.NumberOfRecords);

            return Ok(collection.Paginate(pagination).ToList());
        }

        [HttpDelete]
        [Route("remove-status")]
        public async Task<IActionResult> RemoveStatusAsync([FromQuery] int id)
        {
            try
            {
                await accountStatusService.RemoveStatusAsync(id);
            }
            catch(Exception e)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(e.Message),
                    ReasonPhrase = "Remove status error"

                };

                throw new System.Web.Http.HttpResponseException(response);
            }

            return Ok();
        }

        [HttpPost]
        [Route("add-status")]
        public IActionResult AddNewStatusAsync([FromBody] AccountStatus status)
        {
            try
            {
                accountStatusService.AddStatus(status);
            }
            catch (Exception e)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(e.Message),
                    ReasonPhrase = "Insert status error"

                };

                throw new System.Web.Http.HttpResponseException(response);
            }

            return Ok();
        }

        [HttpPut]
        [Route("edit-status")]
        public IActionResult EditStatusAsync([FromBody] AccountStatus status)
        {
            try
            {
                accountStatusService.EditStatus(status);
            }
            catch (Exception e)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(e.Message),
                    ReasonPhrase = "Edit status error"

                };

                throw new System.Web.Http.HttpResponseException(response);
            }

            return Ok();
        }
    }
}
