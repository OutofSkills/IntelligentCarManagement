using IntelligentCarManagement.Client.Services;
using IntelligentCarManagement.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Client.Pages
{
    public class CarsTableRecordBase : ComponentBase
    {
        [Parameter]
        public Car Car { get; set; }
        [Parameter]
        public EventCallback OnCarRemoved { get; set; }
        [Inject]
        public ICarService CarService { get; set; }

        protected async Task RemoveCar()
        {
            await CarService.RemoveCarAsync(Car.Id);
            await OnCarRemoved.InvokeAsync(Car.Id);
        }
    }
}
