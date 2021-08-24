using IntelligentCarManagement.Models;
using IntelligentCarManagement.Client.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace IntelligentCarManagement.Client.Pages
{
    public class CarsBase: ComponentBase
    {
        [Inject]
        public ICarService CarService { get; set; }
        public IEnumerable<Car> Cars { get; set; }
        public int NumberOfPages { get; set; }
        public int CurrentPage { get; set; } = 1;


        protected override async Task OnInitializedAsync()
        {
            await LoadCars();
        }

        protected async Task DataChanged()
        {
            await LoadCars();
        }

        private async Task LoadCars(int page = 1)
        {
            var httpResponse = await CarService.GetCarsAsync(page);

            NumberOfPages = int.Parse(httpResponse.Headers.GetValues("numberOfPages").FirstOrDefault());

            var responseString = await httpResponse.Content.ReadAsStringAsync();

            Cars = JsonSerializer.Deserialize<IEnumerable<Car>>(responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task SelectedPage(int page)
        {
            CurrentPage = page;
            await LoadCars(page);
        }
    }
}
