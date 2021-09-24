using IntelligentCarManagement.Client.Services;
using IntelligentCarManagement.Client.Shared;
using IntelligentCarManagement.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Client.Pages.Management.Drivers
{
    public class DriverRequestsTableRecordBase: ComponentBase
    {

        [Inject] public IDriverService DriverService { get; private set; }

        [Inject] public IDialogService DialogService { get; set; }

        [Inject] public ISnackbar Snackbar { get; set; }

        [Parameter] public EventCallback OnDataChange { get; set; }

        [Parameter] public Driver Driver { get; set; }

        protected async Task OpenDeleteDialogAsync()
        {
            var parameters = new DialogParameters
            {
                { "Id", Driver.Id },
                { "ContentText", $"Are you sure you want to reject the request with the id {Driver.Id} ?" },
                { "ButtonText", "Reject" },
                { "Color", Color.Error }
            };

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = DialogService.Show<DialogDeleteWindow>("Reject", parameters, options);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                await DeclineDriverRequest();
            }
        }

        protected async Task DeclineDriverRequest()
        {
            var state = await DriverService.DeclineDriverRequest(Driver.Id);

            if (state is true)
            {
                Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomCenter;
                Snackbar.Add("Driver request declined successfully.", Severity.Success);
            }
            else
            {
                Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomCenter;
                Snackbar.Add("Something went wrong. Couldn't decline the request.", Severity.Error);
            }

            await OnDataChange.InvokeAsync(Driver.Id);
        }

        protected async Task OpenAcceptDialogAsync()
        {
            var parameters = new DialogParameters
            {
                { "Id", Driver.Id },
                { "ContentText", $"Are you sure you want to change the status of the request with the id {Driver.Id} to accepted ?" },
                { "ButtonText", "Accept" },
                { "Color", Color.Primary }
            };

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = DialogService.Show<DialogDeleteWindow>("Accept Reuest", parameters, options);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                await AcceptDriverRequest();
            }
        }

        private async Task AcceptDriverRequest()
        {
            var state = await DriverService.AcceptDriverRequest(Driver.Id);

            if (state is true)
            {
                Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomCenter;
                Snackbar.Add("Driver request accepted successfully.", Severity.Success);
            }
            else
            {
                Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomCenter;
                Snackbar.Add("Something went wrong. Couldn't accept the request.", Severity.Error);
            }

            await OnDataChange.InvokeAsync(Driver.Id);
        }
    }
}
