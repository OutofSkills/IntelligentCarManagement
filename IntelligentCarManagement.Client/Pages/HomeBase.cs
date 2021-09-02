using IntelligentCarManagement.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Client.Pages
{
    public class HomeBase : ComponentBase
    {
        protected Ride Ride { get; set; } = new();
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        protected bool displayMap = false;

        protected string DestinationInputIcon = "";
        protected string PickUpInputIcon = Icons.Filled.AddLocationAlt;

        protected enum TaskStates { Started, Ended };
        protected string currentState = "";

        protected override void OnInitialized()
        {
            Ride.PickUpLocation = "";
            Ride.Destination = "";
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("initialize", null);
            }
        }

        protected async Task HandleSubmit()
        {

        }

        protected async Task AutoCompletePickUp()
        {
            await JSRuntime.InvokeVoidAsync("getPickUpLocation", null);
        }
        protected async Task AutoCompleteDestination()
        {
            await JSRuntime.InvokeVoidAsync("getDestinationLocation", null);
        }

        protected void OnPickUpInputChangeAsync(string pickUpLocation)
        {
            Ride.PickUpLocation = pickUpLocation;

            if (pickUpLocation.Length > 0)
            {
                PickUpInputIcon = Icons.Filled.Clear;
            }
            else
            {
                PickUpInputIcon = Icons.Filled.AddLocationAlt;
            }
        }

        protected void OnDestinationInputChange(string destination)
        {
            Ride.Destination = destination;

            if (destination.Length > 0)
            {
                DestinationInputIcon = Icons.Filled.Clear;
            }
            else
            {
                DestinationInputIcon = "";
            }
        }

        protected async Task OpenMapAsync()
        {
            if (PickUpInputIcon == Icons.Filled.AddLocationAlt)
            {
                // Display the map only when asking to find your location 
                displayMap = true;
                // Get the location
                await JSRuntime.InvokeVoidAsync("getCurrentLocation", null);
            }
            else
            {
                //Close the map if there's no location given
                displayMap = false;
                // Clear the input
                ClearInput("PickUp");
            }

            if (Ride.PickUpLocation.Length > 0)
            {
                // Change GPS icon to clear one
                PickUpInputIcon = Icons.Filled.Clear;
            }
            else
            {
                // Change clear icon to GPS one
                PickUpInputIcon = Icons.Filled.AddLocationAlt;
            }
        }

        protected void ClearInput(string id)
        {
            switch (id)
            {
                case "PickUp":
                    Ride.PickUpLocation = "";
                    break;
                case "Destination":
                    Ride.Destination = "";
                    DestinationInputIcon = "";
                    break;
            }
        }
    }
}
