using IntelligentCarManagement.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace IntelligentCarManagement.UI.Services
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
            return await httpClient.GetJsonAsync<Car[]>("api/Car/getCars");
        }
        public async Task<Driver> GetCarDriver(int carId)
        {
            return await httpClient.GetJsonAsync<Driver>($"api/Driver/getDriver/{carId}");
        }
    }
}
