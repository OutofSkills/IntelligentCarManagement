using Client.Authentication;
using Models.DTOs;
using System.Net.Http.Headers;

namespace ClientUI.Services.Clients
{
    public class ClientsService : IClientsService
    {
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;

        public ClientsService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }

        public async Task<IEnumerable<ClientDTO>> GetAllAsync()
        {
            // **************    Set JWT header       ****************
            var token = await tokenService.GetTokenAsync();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.GetAsync("https://intellicarsapi.azurewebsites.net/api/Clients");

            var content = await response.Content.ReadAsStringAsync();

            var result = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<ClientDTO>>(content, new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result;
        }
    }
}
