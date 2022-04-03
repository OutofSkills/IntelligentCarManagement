using IntelligentCarManagement.Client.Services;
using Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using MudBlazor;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Client.Pages
{
    public class HomeBase : ComponentBase
    {
        protected Ride Ride { get; set; } = new();

        [Inject] public IRidesService RidesService { get; set; }
        [Inject] public IJSRuntime JSRuntime { get; set; }
        [Inject] public NavigationManager NavManager { get; set; }
        [Inject] public AuthenticationStateProvider AuthStateProvider { get; set; }
        [Inject] public ISnackbar Snackbar { get; set; }

        protected string DestinationInputIcon = "";
        protected string PickUpInputIcon = Icons.Filled.AddLocationAlt;

        protected enum TaskStates { Started, Ended };
        protected string currentState = "";

        protected override async Task OnInitializedAsync()
        {
            Ride.PickUpLocation = "";
            Ride.Destination = "";

            var dotNetReference = DotNetObjectReference.Create(this);
            await JSRuntime.InvokeVoidAsync("GLOBAL.SetDotnetReference", dotNetReference);
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
            var userId = await GetLoggedUserId();
            if(userId is not 0)
            {
                // Assign the logged user
                Ride.ClientId = userId;
                var rideId = await RidesService.SheduleNewRideAsync(Ride);
                if (!string.IsNullOrEmpty(rideId))
                    NavManager.NavigateTo($"/ride/complete-request/{rideId}");
            }
            else
            {
                Snackbar.Add("Please sign up or login!", Severity.Error);
            }
        }

        protected async Task AutoCompletePickUp()
        {
            await JSRuntime.InvokeVoidAsync("autocompletePickUp", null);
        }
        protected async Task AutoCompleteDestination()
        {
            await JSRuntime.InvokeVoidAsync("autocompleteDestination", null);
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

        protected async Task FindMyLocationAsync()
        {
            if (PickUpInputIcon == Icons.Filled.AddLocationAlt)
            {
                // Get the location
                await JSRuntime.InvokeVoidAsync("getCurrentLocation", null);
            }
            else
            {
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

        [JSInvokable]
        public void AssignPickUpCoordinates(string latlng)
        {
            Ride.PickUpCoordinates = latlng;
        }

        [JSInvokable("AssignDestinationCoordinates")]
        public void AssignDestinationCoordinates(string latlng)
        {
            Ride.DestinationCoordinates = latlng;
        }

        private async Task<int> GetLoggedUserId()
        {
            var authState = await AuthStateProvider.GetAuthenticationStateAsync();
            var userId = authState.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            return int.Parse(userId);
        }

    }
}
