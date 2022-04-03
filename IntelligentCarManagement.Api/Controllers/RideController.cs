using IntelligentCarManagement.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RideController : ControllerBase
    {
        private readonly IRidesService ridesService;

        public RideController(IRidesService ridesService)
        {
            this.ridesService = ridesService;
        }

        [HttpPost]
        [Route("shedule-ride")]
        public IActionResult SheduleNewRide([FromBody] Ride ride)
        {
            int rideId;
            try
            {
                rideId = ridesService.AddRide(ride);
            }
            catch (Exception e)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(e.Message),
                    ReasonPhrase = "Shedule ride error"

                };

                throw new System.Web.Http.HttpResponseException(response);
            }

            return Ok(rideId.ToString());
        }

        [HttpGet]
        [Route("find-ride")]
        public async Task<IActionResult> FindRideAsync([FromQuery] int id)
        {
            Ride ride;
            try
            {
                ride = await ridesService.GetRideAsync(id);
            }
            catch (Exception e)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(e.Message),
                    ReasonPhrase = "Couldn't find the searched ride."

                };

                throw new System.Web.Http.HttpResponseException(response);
            }

            return Ok(ride);
        }
    }
}
