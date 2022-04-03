using IntelligentCarManagement.Client.Services;
using Models;
using Majorsoft.Blazor.Components.Common.JsInterop.Scroll;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace IntelligentCarManagement.Client.Pages
{
    public class AvailableRideDriversBase: ComponentBase
    {
        [Parameter] public string Id { get; set; }
        [Parameter] public Ride Ride { get; set; } = new();

        [Inject] public IRidesService RidesService { get; set; }
        [Inject] public IUsersService UsersService { get; set; }
        [Inject] public IJSRuntime JSRuntime { get; set; }

        protected bool hideDriverList = true;

        protected override async Task OnInitializedAsync()
        {
            Ride = await RidesService.FindRideAsync(int.Parse(Id));

            var pickUpLatLng = Ride.PickUpCoordinates.Split(';');
            var destinationLatLng = Ride.DestinationCoordinates.Split(';');
            await JSRuntime.InvokeVoidAsync("initMarkers", pickUpLatLng, destinationLatLng); // 0 - latitude; 1 - longitude
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("initialize", null);
            }
        }

        protected async Task HandleSubmitAsync()
        {
            hideDriverList = false;
            var result = await UsersService.Edit(Ride.User);

            if(result)
                await JSRuntime.InvokeVoidAsync("scrollToDrivers", null);
        }
    }
}
