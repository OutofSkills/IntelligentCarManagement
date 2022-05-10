using IntelligentCarManagement.Api.Services;
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
    [Route("api/[controller]")]
    [ApiController]
    public class RideController : ControllerBase
    {
        private readonly IRidesService ridesService;
        private readonly INotificationService notificationService;

        public RideController(IRidesService ridesService, INotificationService notificationService )
        {
            this.ridesService = ridesService;
            this.notificationService = notificationService;
        }

        [HttpPost]
        public async Task<RideRequestResponse> RequestRide([FromBody] Ride ride)
        {
            var response = await ridesService.RequestAsync(ride);
    
            if (!response.Success)
                return response;

            var notificationResponse = await notificationService.SendAsync(ride.DriverId, new NotificationDTO { Title = "New available ride", Body = $"User with id {ride.ClientId} requested a new ride."});
            
            if (notificationResponse.IsSuccess)
                return new RideRequestResponse() 
                { 
                    Success = false,
                    Message = "Couldn't notify our driver about the incoming request. Please contact him via one of his contacts" 
                };
            
            return response;
        }

        [HttpGet]
        [Route("find-ride")]
        public async Task<IActionResult> FindRideAsync([FromQuery] int id)
        {
            Ride ride;
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
