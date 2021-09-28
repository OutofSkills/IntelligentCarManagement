using IntelligentCarManagement.Api.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
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
    public class NotificationController : Controller
    {
        private readonly INotificationService notificationService;

        public NotificationController(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        [HttpGet]
        [Route("user-notifications")]
        public async Task<IActionResult> GetUserNotificationsAsync([FromQuery] int userId)
        {
            var notifications = await notificationService.GetUserNotificationsAsync(userId);

            return Ok(notifications);
        }

        [HttpDelete]
        [Route("remove-notification")]
        public async Task<IActionResult>RemoveNotificationAsync([FromQuery] int id)
        {
            try
            {
                await notificationService.RemoveNotificationAsync(id);
            }
            catch (Exception e)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(e.Message),
                    ReasonPhrase = "Remove notification error"

                };

                throw new System.Web.Http.HttpResponseException(response);
            }

            return Ok();
        }
    }
}
