using Client.Authentication;
using ClientUI.Utils;
using Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ClientUI.Services.Statuses
{
    public class StatusesService : IStatusesService
    {
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;

        public StatusesService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        public async Task<RequestResponse> AddAccountStatusAsync(AccountStatus model)
        {
            // **************    Set JWT header       ****************
            var token = await tokenService.GetTokenAsync();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var json = JsonConvert.SerializeObject(model);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("https://intellicarsapi.azurewebsites.net/api/Statuses/account", stringContent);

            if (response.IsSuccessStatusCode == false)
            {
                return new RequestResponse() { IsSuccess = false, Message = "Server error. Please try again later." };
            }

            return new RequestResponse() { IsSuccess = true, Message = "Status added successfully." };
        }

        public async Task<RequestResponse> AddApplicationStatusAsync(ApplicationStatus model)
        {
            // **************    Set JWT header       ****************
            var token = await tokenService.GetTokenAsync();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var json = JsonConvert.SerializeObject(model);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("https://intellicarsapi.azurewebsites.net/api/Statuses/application", stringContent);

            if (response.IsSuccessStatusCode == false)
            {
                return new RequestResponse() { IsSuccess = false, Message = "Server error. Please try again later." };
            }

            return new RequestResponse() { IsSuccess = true, Message = "Status added successfully." };
        }

        public async Task<RequestResponse> AddRideStatusAsync(RideState model)
        {
            // **************    Set JWT header       ****************
            var token = await tokenService.GetTokenAsync();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var json = JsonConvert.SerializeObject(model);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("https://intellicarsapi.azurewebsites.net/api/Statuses/ride", stringContent);

            if (response.IsSuccessStatusCode == false)
            {
                return new RequestResponse() { IsSuccess = false, Message = "Server error. Please try again later." };
            }

            return new RequestResponse() { IsSuccess = true, Message = "Status added successfully." };
        }

        public async Task<IEnumerable<AccountStatus>> GetAccountStatusesAsync()
        {
            // **************    Set JWT header       ****************
            var token = await tokenService.GetTokenAsync();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.GetAsync("https://intellicarsapi.azurewebsites.net/api/Statuses/account");

            var content = await response.Content.ReadAsStringAsync();

            var result = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<AccountStatus>>(content, new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result;
        }

        public async Task<IEnumerable<ApplicationStatus>> GetApplicationStatusesAsync()
        {
            // **************    Set JWT header       ****************
            var token = await tokenService.GetTokenAsync();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.GetAsync("https://intellicarsapi.azurewebsites.net/api/Statuses/application");

            var content = await response.Content.ReadAsStringAsync();

            var result = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<ApplicationStatus>>(content, new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result;
        }

        public async Task<IEnumerable<RideState>> GetRideStatusesAsync()
        {
            // **************    Set JWT header       ****************
            var token = await tokenService.GetTokenAsync();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.GetAsync("https://intellicarsapi.azurewebsites.net/api/Statuses/ride");

            var content = await response.Content.ReadAsStringAsync();

            var result = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<RideState>>(content, new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result;
        }
    }
}
