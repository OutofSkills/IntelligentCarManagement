using IntelligentCarManagement.Models;
using IntelligentCarManagement.Client.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Client.Pages
{
    public class CarsBase: ComponentBase
    {
        [Inject]
        public ICarService CarService { get; set; }
        public IEnumerable<Car> Cars { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Cars = await CarService.GetCars();
        }
    }
}
