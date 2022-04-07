using IntelligentCarManagement.DataAccess.UnitsOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntelligentCarManagement.Services;
using IntelligentCarManagement.Api.Helpers;
using IntelligentCarManagement.Api.Services;
using Microsoft.AspNetCore.Cors;
using System.Net.Http;
using System.Net;
using Models.View_Models;
using Models;
using Models.Data_Transfer_Objects;

namespace IntelligentCarManagement.Api.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly IDriverService driverService;

        public DriverController(IDriverService driverService)
        {
            this.driverService = driverService;
        }

        [HttpGet]
        [Route("byEmail")]
        public DriverDTO GetDriverAsync([FromQuery] string email)
        {
            var user = driverService.Get(email);

            return user;
        }

        [HttpGet]
        [Route("byId")]
        public async Task<DriverDTO> GetDriverAsync([FromQuery] int driverId)
        {
            var user = await driverService.GetAsync(driverId);

            return user;
        }

        [HttpGet]
        public async Task<IEnumerable<DriverDTO>> GetAsync()
        {
            return await driverService.GetAllAsync();
        }
        [HttpPut]
        public async Task<DriverDTO> UpdatAsync([FromQuery] int id, [FromBody] DriverDTO driver)
        {
            return await driverService.UpdateAsync(id, driver);
        }

        [HttpPost]
        public DriverDTO AddDriver(DriverDTO driver)
        {
            return driverService.Add(driver);
        }
    }
}
