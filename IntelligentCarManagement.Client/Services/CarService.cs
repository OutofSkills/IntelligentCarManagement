using IntelligentCarManagement.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
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

        public async Task<IEnumerable<Car>> GetCars()
        {
            httpClient.BaseAddress = new Uri("http://localhost:41427");
            IEnumerable <Car> cars = await httpClient.GetJsonAsync<Car[]>("/api/Cars/GetCars");

            return cars;
        }
        public async Task<Driver> GetCarDriver(int carId)
        {
            return await httpClient.GetJsonAsync<Driver>($"/api/Driver/GetDriver/{carId}");
        }
    }
}
