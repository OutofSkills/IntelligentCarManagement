using IntelligentCarManagement.Models;
using IntelligentCarManagement.DataAccess.UnitsOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntelligentCarManagement.Services;

namespace IntelligentCarManagement.Api.Controllers
{
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
