using Api.Services.Utils;
using IntelligentCarManagement.Api.Services;
using IntelligentCarManagement.Services;
using Microsoft.AspNetCore.Cors;
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
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : Controller
    {
        private readonly INotificationService notificationService;
        private readonly IDriversService driversService;
        private readonly IClientsService clientsService;

        public NotificationsController(INotificationService notificationService, IDriversService driversService, IClientsService clientsService)
        {
            this.notificationService = notificationService;
            this.driversService = driversService;
            this.clientsService = clientsService;
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

        [Route("send/category")]
        [HttpPost]
        public async Task SendNotification(NotificationDTO notification, [FromQuery] string category)
        {
            var notificationCategory = await notificationService.GetNotificationCategoryAsync(NotificationCategories.GENERAL);
            notification.NotificationCategory = notificationCategory;

            var clients = await clientsService.GetAllAsync();
            var drivers = await driversService.GetAllAsync(null);

            switch (category)
            {
                case "All":
                    foreach(var client in clients)
                    {
                        try
                        {
                            var result = await notificationService.SendAsync(client.Id, notification);
                        }catch (Exception e)
                        {
                            // log the exception
                        }
                    }
                    foreach (var driver in drivers)
                    {
                        try
                        {
                            var result = await notificationService.SendAsync(driver.Id, notification);
                        }
                        catch (Exception e)
                        {
                            // log the exception
                        }
                    }
                    break;
                case "Drivers":
                    foreach (var driver in drivers)
                    {
                        try
                        {
                            var result = await notificationService.SendAsync(driver.Id, notification);
                        }
                        catch (Exception e)
                        {
                            // log the exception
                        }
                    }
                    break;
                case "Clients":
                    foreach (var client in clients)
                    {
                        try
                        {
                            var result = await notificationService.SendAsync(client.Id, notification);
                        }
                        catch (Exception e)
                        {
                            // log the exception
                        }
                    }
                    break;
            }
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
        public NotificationCategory CreateCategoryAsync([FromBody] NotificationCategory category)
        {
            notificationService.CreateCategory(category);

            return category;
        }

        [Route("category/id")]
        [HttpPut]
        public NotificationCategory EditCategoryAsync([FromQuery] int id, [FromBody] NotificationCategory category)
        {
            notificationService.UpdateCategoryAsync(category, id);

            return category;
        }

        [Route("category/id")]
        [HttpDelete]
        public async Task RemoveCategoryAsync([FromQuery] int id)
        {
            await notificationService.RemoveCategoryAsync(id);
        }

        [Route("category")]
        [HttpGet]
        public async Task<IEnumerable<NotificationCategory>> GetCategoriesAsync()
        {
            return await notificationService.GetCategoriesAsync();
        }
    }
}
