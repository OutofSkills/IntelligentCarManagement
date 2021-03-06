using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Client.Services
{
    public class DriverService : IDriverService
    {
        private readonly HttpClient httpClient;

        public DriverService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<bool> AddDriver(Driver driver)
        {
            var result = await httpClient.PostAsJsonAsync("/api/Driver/add-driver", driver);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateDriverStatus(int id, string status)
        {
            var result = await httpClient.GetAsync($"/api/Driver/update-request?id={id}&status={status}");
            return result.IsSuccessStatusCode;
        }

        public async Task<HttpResponseMessage> GetDriversAsync(int page = 1, int recordsPerPage = 10)
        {
            var response = await httpClient.GetAsync($"api/Driver/get-drivers?page={page}&recordsPerPage={recordsPerPage}");

            return response;
        }

        public async Task<bool> UpdateDriver(Driver driver)
        {
            var result = await httpClient.PutAsJsonAsync("/api/Driver/update-driver", driver);
            return result.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<Driver>> GetAllDriversAsync()
        {
            var drivers = await httpClient.GetFromJsonAsync<Driver[]>($"/api/Driver/get-all-drivers");

            return drivers;
        }
    }
}
