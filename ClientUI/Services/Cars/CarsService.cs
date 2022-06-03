using ClientUI.Utils;
using Models.Data_Transfer_Objects;
using Newtonsoft.Json;
using System.Text;

namespace ClientUI.Services.Cars
{
    public class CarsService : ICarsService
    {
        private readonly HttpClient httpClient;

        public CarsService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<RequestResponse> AssignCarAsync(int carId, string driverEmail)
        {
            var response = await httpClient.GetAsync($"https://intellicarsapi.azurewebsites.net/api/Cars/assign?carId={carId}&&driverEmail={driverEmail}");

            if (response.IsSuccessStatusCode == false)
            {
                return new RequestResponse() { IsSuccess = false, Message = "Server error. Please try again later." };
            }

            return new RequestResponse() { IsSuccess = true, Message = "Car assigned successfully." };
        }

        public async Task<RequestResponse> CreateAsync(CarDTO dto)
        {
            var json = JsonConvert.SerializeObject(dto);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("https://intellicarsapi.azurewebsites.net/api/Cars", stringContent);

            if (response.IsSuccessStatusCode == false)
            {
                return new RequestResponse() { IsSuccess = false, Message = "Server error. Please try again later." };
            }

            return new RequestResponse() { IsSuccess = true, Message = "Application sent successfully." };
        }

        public async Task<IEnumerable<CarDTO>> GetAllAsync()
        {
            var response = await httpClient.GetAsync("https://intellicarsapi.azurewebsites.net/api/Cars");

            var content = await response.Content.ReadAsStringAsync();

            var result = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<CarDTO>>(content, new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result;
        }
    }
}
