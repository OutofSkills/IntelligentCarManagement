using IntelligentCarManagement.Client.Services;
using IntelligentCarManagement.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace IntelligentCarManagement.Client.Pages
{
    public class NewCarBase : ComponentBase
    {
        [Inject]
        public ICarService CarService { get; set; }
        [Inject]
        public NavigationManager NavManager { get; set; }
        protected Car Car = new();

        public async Task AddNewCarAsync()
        {
            var response = await CarService.AddNewCar(Car);

            if(response is true)
            {
                NavManager.NavigateTo("/cars");
            }
        }
    }
}
