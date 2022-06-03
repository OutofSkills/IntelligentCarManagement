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
using Models.DTOs;
using Models;
using Models.DTOs;

namespace IntelligentCarManagement.Api.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly IDriversService driverService;

        public DriversController(IDriversService driverService)
        {
            this.driverService = driverService;
        }

        [HttpGet]
        [Route("byEmail")]
        public DriverDTO GetAsync([FromQuery] string email)
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
        public async Task<IEnumerable<DriverDTO>> GetAsync([FromQuery] bool? availability)
        {
            return await driverService.GetAllAsync(availability);
        }

        [HttpPut]
        public async Task<DriverDTO> UpdatAsync([FromQuery] int id, [FromBody] DriverDTO driver)
        {
            return await driverService.UpdateAsync(id, driver);
        }

        [HttpPost]
        public DriverDTO Add(DriverDTO driver)
        {
            return driverService.Add(driver);
        }

        [HttpPut]
        [Route("availability")]
        public async Task<bool> MakeAvailable([FromQuery] int id, [FromQuery] bool isAvailable)
        {
            await driverService.BecomeAvailable(id, isAvailable);

            return isAvailable;
        }

        [HttpGet]
        [Route("availability")]
        public async Task<bool> IsAvailable([FromQuery] int id)
        {
            return await driverService.IsAvailable(id);
        }

        [HttpPut]
        [Route("location")]
        public async Task UpdateLocation([FromQuery] int id, [FromQuery] string latitude, [FromQuery] string longitude)
        {
            await driverService.UpdateLocationAsync(id, latitude, longitude);
        }
    }
}
