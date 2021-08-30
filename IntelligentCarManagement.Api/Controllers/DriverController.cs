using IntelligentCarManagement.Models;
using IntelligentCarManagement.DataAccess.UnitsOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntelligentCarManagement.Services;
using IntelligentCarManagement.Models.NotMapped_Models;
using IntelligentCarManagement.Api.Helpers;
using IntelligentCarManagement.Api.Services;
using Microsoft.AspNetCore.Cors;

namespace IntelligentCarManagement.Api.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly IDriverService driverService;
        private readonly IDriverStatusService driverStatusService;

        public DriverController(IDriverService driverService, IDriverStatusService driverStatusService)
        {
            this.driverService = driverService;
            this.driverStatusService = driverStatusService;
        }

        [HttpGet]
        [Route("get-drivers")]
        public async Task<IActionResult> GetDriversAsync([FromQuery] Pagination pagination)
        {
            var collection = await driverService.GetDrivers();

            HttpContext.InsertPaginationParameterInResponse(collection, pagination.NumberOfRecords);

            return Ok(collection.Paginate(pagination).ToList());
        }

        [HttpGet]
        [Route("get-all-drivers")]
        public async Task<IActionResult> GetDriversAsync()
        {
            return Ok(await driverService.GetDrivers());
        }

        [HttpGet]
        [Route("accept-request")]
        public async Task<IActionResult> AcceptDriverAsync([FromQuery]int id)
        {
            var driver = await driverService.GetDriver(id);
            var status = driverStatusService.GetStatusByName("ACCEPTED");

            driver.Status = status;

            var result = driverService.UpdateDriver(driver);


            if (result is true)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("decline-request")]
        public async Task<IActionResult> DeclineDriverAsync([FromQuery]int id)
        {
            var driver = await driverService.GetDriver(id);
            var status = driverStatusService.GetStatusByName("REJECTED");

            driver.Status = status;

            var result = driverService.UpdateDriver(driver);


            if (result is true)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("getDriver/{carId}")]
        public IActionResult GetDriver(int carId)
        {
            return Ok(driverService.GetCarDriver(carId));
        }

        [HttpPost]
        [Route("add-driver")]
        public IActionResult AddDriver(Driver driver)
        {
            var result = driverService.AddDriver(driver);

            if(result is true)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("update-driver")]
        public IActionResult UpdateDriver(Driver driver)
        {
            var result = driverService.UpdateDriver(driver);

            if (result is true)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
