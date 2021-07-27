using IntelligentCarManagement.DataModels;
using IntelligentCarManagement.UI.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentCarManagement.UI.Pages
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
