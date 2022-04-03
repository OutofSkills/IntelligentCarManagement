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
        [Route("update-request")]
        public async Task<IActionResult> UpdateDriverStatusAsync([FromQuery]int id, string status)
        {
            try
            {
                await driverService.ChangeDriverStatusAsync(id, status);
            }
            catch (Exception e)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(e.Message),
                    ReasonPhrase = "Update driver status error"

                };

                throw new System.Web.Http.HttpResponseException(response);
            }

            return Ok();
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
            try
            {
                driverService.AddDriver(driver);
            }
            catch (Exception e)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(e.Message),
                    ReasonPhrase = "Insert driver status error"

                };

                throw new System.Web.Http.HttpResponseException(response);
            }

            return Ok();
        }

        [HttpPut]
        [Route("update-driver")]
        public IActionResult UpdateDriver(Driver driver)
        {
            try
            {
                driverService.UpdateDriver(driver);
            }
            catch (Exception e)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(e.Message),
                    ReasonPhrase = "Update driver error"

                };

                throw new System.Web.Http.HttpResponseException(response);
            }

            return Ok();
        }
    }
}
