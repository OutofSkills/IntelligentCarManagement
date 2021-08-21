using IntelligentCarManagement.Models;
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

        public async Task<bool> UpdateDriver(Driver driver)
        {
            var result = await httpClient.PostAsJsonAsync("/api/Driver/update-driver", driver);
            return result.IsSuccessStatusCode;
        }
    }
}
