using IntelligentCarManagement.Api.Helpers;
using IntelligentCarManagement.Api.Services;
using IntelligentCarManagement.Models;
using IntelligentCarManagement.Models.NotMapped_Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet]
        [Route("remove-status")]
        public async Task<IActionResult> RemoveStatusAsync([FromQuery] int id)
        {
            var result = await accountStatusService.RemoveStatusAsync(id);

            if (result is true)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("add-status")]
        public IActionResult AddNewStatusAsync([FromBody] AccountStatus status)
        {
            var result = accountStatusService.AddStatus(status);

            if (result is true)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("edit-status")]
        public IActionResult EditStatusAsync([FromBody] AccountStatus status)
        {
            var result = accountStatusService.EditStatusAsync(status);

            if (result is true)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
