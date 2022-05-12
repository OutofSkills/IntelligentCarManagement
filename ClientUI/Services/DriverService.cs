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

        public async Task<RequestResponse> SubmitDriverApplication(DriverApplicationDTO model)
        {
            var json = JsonConvert.SerializeObject(model);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("http://localhost:41427/api/DriverApplications", stringContent);

            if (response.IsSuccessStatusCode == false)
            {
                return new RequestResponse() { IsSuccess = false, Message = "Server error. Please try again later." };
            }

            return new RequestResponse() { IsSuccess = true, Message = "Application sent successfully." };
        }
    }
}
