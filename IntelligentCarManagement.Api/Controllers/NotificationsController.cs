using Api.Services.Utils;
using IntelligentCarManagement.Api.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
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
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : Controller
    {
        private readonly INotificationService notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        [HttpGet]
        public async Task<IEnumerable<NotificationDTO>> GetUserNotificationsAsync([FromQuery] int userId)
        {
            var notifications = await notificationService.GetAsync(userId);

            return notifications;
        }

        [HttpDelete]
        public async Task<IActionResult>RemoveNotificationAsync([FromQuery] int id)
        {
            try
            {
                await notificationService.RemoveAsync(id);
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

        [Route("send/{userId}")]
        [HttpPost]
        public async Task<IActionResult> SendNotification(NotificationDTO notification, int userId)
        {
            var notificationCategory = await notificationService.GetNotificationCategoryAsync(NotificationCategories.GENERAL);
            notification.NotificaionCategory = notificationCategory;

            var result = await notificationService.SendAsync(userId, notification);
            return Ok(result);
        }

        [Route("token")]
        [HttpPut]
        public async Task<IActionResult> UpdateToken([FromQuery]int id, [FromBody]string firebaseToken)
        {
             await notificationService.UpdateFirebaseToken(id, firebaseToken);

            return Ok(firebaseToken);
        }

        [Route("category")]
        [HttpPost]
        public NotificationCategoryDTO CreateCategory([FromBody] NotificationCategoryDTO categoryDTO)
        {
            notificationService.CreateCategory(categoryDTO);

            return categoryDTO;
        }
    }
}
