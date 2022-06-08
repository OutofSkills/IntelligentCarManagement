using Client.Authentication;
using Models.Data_Transfer_Objects;
using System.Net.Http.Headers;

namespace ClientUI.Services.Rides
{
    public class RidesService : IRidesService
    {
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;

        public RidesService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }

        public async Task<IEnumerable<RideDTO>> GetAllAsync()
        {
            // **************    Set JWT header       ****************
            var token = await tokenService.GetTokenAsync();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.GetAsync("https://intellicarsapi.azurewebsites.net/api/Rides");

            var content = await response.Content.ReadAsStringAsync();

            var result = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<RideDTO>>(content, new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result;
        }
    }
}
