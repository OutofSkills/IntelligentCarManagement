using IntelligentCarManagement.Client.Services;
using Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace IntelligentCarManagement.Client.Pages.Management.Cars
{
    public class NewCarBase : ComponentBase
    {
        protected Car Car = new();
        [Inject]
        public ICarService CarService { get; set; }
        [Parameter]
        public EventCallback OnDataChanged { get; set; }

        protected string message;
        protected bool isSuccess = false;
        protected bool isFail = false;

        protected async Task AddCar()
        {
            var state = await CarService.AddNewCar(Car);

            if (state is true)
            {
                isSuccess = true;
                message = "The car was added successfully";
            }
            else
            {
                isFail = true;
                message = "Something went wrong. Couldn't add the car.";
            }

            await OnDataChanged.InvokeAsync(Car.Id);
        }
    }
}
