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
    public class StatusesController : ControllerBase
    {
        private readonly IStatusesService accountStatusService;

        public StatusesController(IStatusesService accountStatusService)
        {
            this.accountStatusService = accountStatusService;
        }

        [HttpGet]
        [Route("account")]
        public async Task<IEnumerable<AccountStatus>> GetStatusesAsync()
        {
            return await accountStatusService.GetAccountStatusesAsync();
        }

        [HttpGet]
        [Route("ride")]
        public async Task<IEnumerable<RideState>> GetRideStatusesAsync()
        {
            return await accountStatusService.GetRideStatusesAsync();
        }

        [HttpGet]
        [Route("application")]
        public async Task<IEnumerable<ApplicationStatus>> GetApplicationStatusesAsync()
        {
            return await accountStatusService.GetApplicationStatusesAsync();
        }

        [HttpDelete]
        [Route("account")]
        public async Task RemoveStatusAsync([FromQuery] int id)
        {
            await accountStatusService.RemoveAccountStatusAsync(id);
        }

        [HttpPost]
        [Route("account")]
        public void AddStatusAsync([FromBody] AccountStatus status)
        {
            accountStatusService.AddAccountStatus(status);
        }

        [HttpPost]
        [Route("ride")]
        public void AddRideStatusAsync([FromBody] RideState status)
        {
            accountStatusService.AddRideStatus(status);
        }

        [HttpPost]
        [Route("application")]
        public void AddApplicationStatusAsync([FromBody] ApplicationStatus status)
        {
            accountStatusService.AddApplicationStatus(status);
        }
    }
}
