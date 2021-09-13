using IntelligentCarManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Client.Services
{
    public class NotificationsService : INotificationsService
    {
        private readonly HttpClient httpClient;

        public NotificationsService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<Notification>> GetUserNotifications(int userId)
        {
            var notifications = await httpClient.GetFromJsonAsync<Notification[]>($"/api/Notification/user-notifications?userId={userId}");

            return notifications;
        }

        public async Task<bool> RemoveNotification(int id)
        {
            var response = await httpClient.DeleteAsync($"/api/Notification/remove-notification?id={id}");

            return response.IsSuccessStatusCode;
        }
    }
}
