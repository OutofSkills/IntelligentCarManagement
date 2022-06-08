using Models.Data_Transfer_Objects;
using Models.DTOs;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

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

        public async Task<RequestResponse> Register(AdminRegisterModel model)
        {
            var json = JsonConvert.SerializeObject(model);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://intellicarsapi.azurewebsites.net/api/AdminAccount/register", stringContent);

            if (response.IsSuccessStatusCode == false)
            {
                return new RequestResponse() { Success = false, Message = "Server error. Please try again later." };
            }

            return new RequestResponse() { Success = true, Message = "Account created successfully." };
        }
    }
}
