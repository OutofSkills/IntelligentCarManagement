using IntelligentCarManagement.Api.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var result = await notificationService.RemoveNotificationAsync(id);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
