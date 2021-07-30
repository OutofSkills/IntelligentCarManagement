using IntelligentCarManagement.Models;
using IntelligentCarManagement.UI.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentCarManagement.UI.Pages
{
    public class DriverDetailsBase: ComponentBase
    {
        [Inject]
        public ICarService CarService { get; set; }

        [Parameter]
        public string Id { get; set; }
        public Driver Driver { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Driver = await CarService.GetCarDriver(int.Parse(Id));
        }
    }
}
