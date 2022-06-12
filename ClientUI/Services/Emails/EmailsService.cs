using Client.Authentication;
using ClientUI.Utils;
using Models.Data_Transfer_Objects;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ClientUI.Services.Emails
{
    public class EmailsService : IEmailsService
    {
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;

        public EmailsService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }

        public async Task<RequestResponse> SendAsync(EmailDTO dto)
        {
            // **************    Set JWT header       ****************
            var token = await tokenService.GetTokenAsync();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var json = JsonConvert.SerializeObject(dto);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("https://intellicarsapi.azurewebsites.net/api/Emails", stringContent);

            if (response.IsSuccessStatusCode == false)
            {
                return new RequestResponse() { IsSuccess = false, Message = "Server error. Please try again later." };
            }

            return new RequestResponse() { IsSuccess = true, Message = "Role added successfully." };
        }
    }
}
