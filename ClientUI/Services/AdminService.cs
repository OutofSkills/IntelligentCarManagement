using Models.DTOs;
using System.Security.Claims;

namespace ClientUI.Services
{
    public class AdminService : IAdminService
    {
        private readonly HttpClient _httpClient;

        public AdminService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<UserBaseDTO>> GetAllAsync()
        {
            var result = await _httpClient.GetAsync($"https://intellicarsapi.azurewebsites.net/api/AdminAccount");
            var content = await result.Content.ReadAsStringAsync();

            var users = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<UserBaseDTO>>(content, new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return users;
        }

        public async Task<UserBaseDTO> GetAsync(int id)
        {
            var result = await _httpClient.GetAsync($"https://intellicarsapi.azurewebsites.net/api/AdminAccount/id?id={id}");
            var content = await result.Content.ReadAsStringAsync();

            var user = System.Text.Json.JsonSerializer.Deserialize<UserBaseDTO>(content, new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return user;
        }
    }
}
