using ClientUI.Utils;
using Models.DTOs;
using Newtonsoft.Json;
using System.Text;

namespace ClientUI.Services
{
    public class DriverService : IDriverService
    {
        private readonly HttpClient httpClient;

        public DriverService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<DriverApplicationDTO>> GetApplicationsAsync()
        {
            var response = await httpClient.GetAsync("http://localhost:41427/api/DriverApplications");

            var content = await response.Content.ReadAsStringAsync();

            var result = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<DriverApplicationDTO>>(content, new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result;
        }

        public async Task<DriverApplicationDTO> GetApplicationAsync(int id)
        {
            var response = await httpClient.GetAsync($"http://localhost:41427/api/DriverApplications/id?id={id}");

            var content = await response.Content.ReadAsStringAsync();

            var result = System.Text.Json.JsonSerializer.Deserialize<DriverApplicationDTO>(content, new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result;
        }

        public async Task<Utils.RequestResponse> SubmitApplication(DriverApplicationDTO model)
        {
            var json = JsonConvert.SerializeObject(model);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("http://localhost:41427/api/DriverApplications", stringContent);

            if (response.IsSuccessStatusCode == false)
            {
                return new Utils.RequestResponse() { IsSuccess = false, Message = "Server error. Please try again later." };
            }

            return new Utils.RequestResponse() { IsSuccess = true, Message = "Application sent successfully." };
        }

        public async Task<Utils.RequestResponse> ApproveApplicationAsync(int id)
        {
            var response = await httpClient.GetAsync($"http://localhost:41427/api/DriverApplications/approve/id?id={id}");

            if (response.IsSuccessStatusCode == false)
            {
                return new Utils.RequestResponse() { IsSuccess = false, Message = "Server error. Please try again later." };
            }

            return new Utils.RequestResponse() { IsSuccess = true, Message = "Application approved successfully." };
        }

        public async Task<Utils.RequestResponse> RejectApplicationAsync(int id)
        {
            var response = await httpClient.GetAsync($"http://localhost:41427/api/DriverApplications/reject/id?{id}");

            if (response.IsSuccessStatusCode == false)
            {
                return new Utils.RequestResponse() { IsSuccess = false, Message = "Server error. Please try again later." };
            }

            return new Utils.RequestResponse() { IsSuccess = true, Message = "Application rejected successfully." };
        }
    }
}
