using IntelligentCarManagement.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Data_Transfer_Objects;
using Models.DTOs;
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
    public class RidesController : ControllerBase
    {
        private readonly IRidesService ridesService;

        public RidesController(IRidesService ridesService)
        {
            this.ridesService = ridesService;
        }

        [HttpPost]
        public async Task<RequestResponse> RequestRide([FromBody] RideDTO ride)
        {
            var response = await ridesService.RequestAsync(ride);
            return response;
        }

        [HttpGet]
        [Route("client")]
        public async Task<IEnumerable<RideDTO>> GetClientRidesAsync([FromQuery] int id)
        {
            var rides = await ridesService.GetClientsAllAsync(id);
       
            return rides;
        }

        [HttpGet]
        [Route("driver")]
        public async Task<IEnumerable<RideDTO>> GetDriverRidesAsync([FromQuery] int id)
        {
            var rides = await ridesService.GetDriversAllAsync(id);

            return rides;
        }

        [HttpGet]
        [Route("find-ride")]
        public async Task<IActionResult> FindRideAsync([FromQuery] int id)
        {
            RideDTO ride;
            try
            {
                ride = await ridesService.GetAsync(id);
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
