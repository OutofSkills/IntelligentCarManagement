using IntelligentCarManagement.Models;
using IntelligentCarManagement.Client.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using MudBlazor;

namespace IntelligentCarManagement.Client.Pages.Management.Cars
{
    public class CarsBase: ComponentBase
    {
        [Inject]
        public ICarService CarService { get; set; }
        [Inject]
        public IDialogService DialogService { get; set; }
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

        protected async Task OpenAddDialogAsync()
        {
            var parameters = new DialogParameters();

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, FullWidth = true };

            var dialog = DialogService.Show<NewCar>("New Car", parameters, options);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                await DataChanged();
            }
        }
    }
}
