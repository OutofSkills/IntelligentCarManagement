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
        public EventCallback OnDataChange { get; set; }
        [Inject]
        public ICarService CarService { get; set; }

        protected string message;
        protected bool isSuccess = false;
        protected bool isFail = false;

        protected async Task RemoveCar()
        {
            await CarService.RemoveCarAsync(Car.Id);
            await OnDataChange.InvokeAsync(Car.Id);
        }

        protected async Task EditCar()
        {
            var state = await CarService.EditCarAsync(Car);

            if (state is true)
            {
                isSuccess = true;
                message = "The status was updated successfully.";
            }
            else
            {
                isFail = true;
                message = "Something went wrong. Couldn't update the status";
            }

            await OnDataChange.InvokeAsync(Car.Id);
        }
    }
}
