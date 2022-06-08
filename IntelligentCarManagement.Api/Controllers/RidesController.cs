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
        public async Task<IEnumerable<RideDTO>> GetAll()
        {
            var rides = await ridesService.GetAllAsync();

            return rides;
        }

        [HttpGet]
        [Route("id")]
        public async Task<RideDTO> FindRideAsync([FromQuery] int id)
        {
            var ride = await ridesService.GetAsync(id);
            
            return ride;
        }

        [HttpPost]
        [Route("confirm")]
        public async Task<RideDTO> ConfirmRequestAsync([FromQuery] int id)
        {
            await ridesService.ConfirmRequestAsync(id);

            return await ridesService.GetAsync(id);
        }

        [HttpPost]
        [Route("end")]
        public async Task<RideDTO> EndRequestAsync([FromQuery] int id)
        {
            await ridesService.EndAsync(id);

            return await ridesService.GetAsync(id);
        }

        [HttpGet]
        [Route("ongoing")]
        public async Task<RideDTO> GetOngoingAsync([FromQuery] int id)
        {
            var ride = await ridesService.GetOngoingAsync(id);

            return ride;
        }
    }
}
