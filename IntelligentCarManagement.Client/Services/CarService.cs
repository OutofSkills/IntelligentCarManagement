using IntelligentCarManagement.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Client.Services
{
    public class CarService : ICarService
    {
        private readonly HttpClient httpClient;

        public CarService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> GetCarsAsync(int page = 1, int recordsPerPage = 10)
        {
            var response = await httpClient.GetAsync($"/api/Cars/GetCars?page={page}&recordsPerPage={recordsPerPage}");
            return response;
        }
        public async Task<Driver> GetCarDriver(int carId)
        {
            return await httpClient.GetJsonAsync<Driver>($"/api/Driver/GetDriver/{carId}");
        }

        public async Task<bool> RemoveCarAsync(int carId)
        {
            var data = new FormUrlEncodedContent(new[]
             {
                new KeyValuePair<string, string>("carId", carId.ToString())
            });

            var response = await httpClient.PostAsync($"/api/Cars/removeCar", data);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AddNewCar(Car car)
        {
            var response = await httpClient.PostAsJsonAsync($"/api/Cars/newCar", car);

            return response.IsSuccessStatusCode;
        }
    }
}
