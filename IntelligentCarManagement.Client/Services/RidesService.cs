using IntelligentCarManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Client.Services
{
    public class RidesService : IRidesService
    {
        private readonly HttpClient httpClient;

        public RidesService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public Task<Ride> FindRideAsync(int id)
        {
            var ride = httpClient.GetFromJsonAsync<Ride>($"/api/Ride/find-ride?id={id}");

            return ride;
        }

        public async Task<string> SheduleNewRideAsync(Ride ride)
        {
            var response = await httpClient.PostAsJsonAsync("/api/Ride/shedule-ride", ride);

            if(response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return responseString;
            }

            return "";
        }
    }
}
