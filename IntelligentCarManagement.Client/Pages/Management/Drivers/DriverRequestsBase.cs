using IntelligentCarManagement.Client.Services;
using Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Client.Pages.Management.Drivers
{
    public class DriverRequestsBase: ComponentBase
    {
        [Inject]
        public IDriverService DriverService { get; set; }

        public IEnumerable<Driver> Drivers { get; set; }

        public int NumberOfPages { get; set; }
        public int CurrentPage { get; set; } = 1;

        protected override async Task OnInitializedAsync()
        {
            await LoadDrivers();
        }

        public async Task SelectedPage(int page)
        {
            CurrentPage = page;
            await LoadDrivers(page);
        }

        private async Task LoadDrivers(int page = 1)
        {
            var httpResponse = await DriverService.GetDriversAsync(page);

            NumberOfPages = int.Parse(httpResponse.Headers.GetValues("numberOfPages").FirstOrDefault());

            var responseString = await httpResponse.Content.ReadAsStringAsync();

            Drivers = JsonSerializer.Deserialize<IEnumerable<Driver>>(responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task DataChanged()
        {
            await LoadDrivers();
        }
    }
}
