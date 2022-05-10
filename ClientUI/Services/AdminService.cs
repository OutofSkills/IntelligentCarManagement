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

        public async Task<UserBaseDTO> Get(int id)
        {
            var result = await _httpClient.GetAsync($"http://localhost:41427/api/AdminAccount?id={id}");
            var content = await result.Content.ReadAsStringAsync();

            var user = System.Text.Json.JsonSerializer.Deserialize<UserBaseDTO>(content, new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return user;
        }
    }
}
