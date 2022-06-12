using Client.Authentication;
using ClientUI.Utils;
using Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ClientUI.Services.Notifications
{
    public class NotificationsService : INotificationsService
    {
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;

        public NotificationsService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }

        public async Task<RequestResponse> AddCategoryAsync(NotificationCategory category)
        {
            // **************    Set JWT header       ****************
            var token = await tokenService.GetTokenAsync();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var json = JsonConvert.SerializeObject(category);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("https://intellicarsapi.azurewebsites.net/api/Notifications/category", stringContent);

            if (response.IsSuccessStatusCode == false)
            {
                return new RequestResponse() { IsSuccess = false, Message = "Server error. Please try again later." };
            }

            return new RequestResponse() { IsSuccess = true, Message = "Category added successfully." };
        }

        public async Task<RequestResponse> DeleteCategoryAsync(int id)
        {
            // **************    Set JWT header       ****************
            var token = await tokenService.GetTokenAsync();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.DeleteAsync($"https://intellicarsapi.azurewebsites.net/api/Notifications/category/id?id={id}");

            if (response.IsSuccessStatusCode == false)
            {
                return new RequestResponse() { IsSuccess = false, Message = "Server error. Please try again later." };
            }

            return new RequestResponse() { IsSuccess = true, Message = "Notification sent successfully." };
        }

        public async Task<RequestResponse> EditCategoryAsync(int id, NotificationCategory category)
        {
            // **************    Set JWT header       ****************
            var token = await tokenService.GetTokenAsync();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var json = JsonConvert.SerializeObject(category);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync($"https://intellicarsapi.azurewebsites.net/api/Notifications/category/id?id={id}", stringContent);

            if (response.IsSuccessStatusCode == false)
            {
                return new RequestResponse() { IsSuccess = false, Message = "Server error. Please try again later." };
            }

            return new RequestResponse() { IsSuccess = true, Message = "Notification edited successfully." };
        }

        public async Task<IEnumerable<NotificationCategory>> GetCategoriesAsync()
        {
            // **************    Set JWT header       ****************
            var token = await tokenService.GetTokenAsync();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.GetAsync("https://intellicarsapi.azurewebsites.net/api/Notifications/category");

            var content = await response.Content.ReadAsStringAsync();

            var result = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<NotificationCategory>>(content, new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result;
        }

        public async Task<RequestResponse> SendAsync(string category, Models.DTOs.NotificationDTO notification)
        {
            // **************    Set JWT header       ****************
            var token = await tokenService.GetTokenAsync();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var json = JsonConvert.SerializeObject(notification);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync($"https://intellicarsapi.azurewebsites.net/api/Notifications/send/category?category={category}", stringContent);

            if (response.IsSuccessStatusCode == false)
            {
                return new RequestResponse() { IsSuccess = false, Message = "Server error. Please try again later." };
            }

            return new RequestResponse() { IsSuccess = true, Message = "Notification sent successfully." };
        }
    }
}
