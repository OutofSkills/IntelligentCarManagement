using Client.Authentication;
using ClientUI.Utils;
using Models.Data_Transfer_Objects;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ClientUI.Services.Roles
{
    public class RolesService : IRolesService
    {
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;

        public RolesService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }

        public async Task<RequestResponse> Add(RoleDTO role)
        {
            // **************    Set JWT header       ****************
            var token = await tokenService.GetTokenAsync();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var json = JsonConvert.SerializeObject(role);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("https://intellicarsapi.azurewebsites.net/api/Roles", stringContent);

            if (response.IsSuccessStatusCode == false)
            {
                return new RequestResponse() { IsSuccess = false, Message = "Server error. Please try again later." };
            }

            return new RequestResponse() { IsSuccess = true, Message = "Role added successfully." };

        }

        public async Task<RequestResponse> Delete(int id)
        {
            // **************    Set JWT header       ****************
            var token = await tokenService.GetTokenAsync();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.DeleteAsync($"https://intellicarsapi.azurewebsites.net/api/Roles/id?id={id}");

            if (response.IsSuccessStatusCode == false)
            {
                return new RequestResponse() { IsSuccess = false, Message = "Server error. Please try again later." };
            }

            return new RequestResponse() { IsSuccess = true, Message = "Role added successfully." };
        }

        public async Task<RequestResponse> Edit(int id, RoleDTO role)
        {
            // **************    Set JWT header       ****************
            var token = await tokenService.GetTokenAsync();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var json = JsonConvert.SerializeObject(role);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync($"https://intellicarsapi.azurewebsites.net/api/Roles/id?id={id}", stringContent);

            if (response.IsSuccessStatusCode == false)
            {
                return new RequestResponse() { IsSuccess = false, Message = "Server error. Please try again later." };
            }

            return new RequestResponse() { IsSuccess = true, Message = "Role added successfully." };
        }

        public async Task<IEnumerable<RoleDTO>> GetAllAsync()
        {
            // **************    Set JWT header       ****************
            var token = await tokenService.GetTokenAsync();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.GetAsync("https://intellicarsapi.azurewebsites.net/api/Roles");

            var content = await response.Content.ReadAsStringAsync();

            var result = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<RoleDTO>>(content, new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result;
        }
    }
}
