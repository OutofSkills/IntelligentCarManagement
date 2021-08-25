using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntelligentCarManagement.Models;
using IntelligentCarManagement.Client.Services;
using MudBlazor;

namespace IntelligentCarManagement.Client.Pages
{
    public class AccountStatusTableRecordBase: ComponentBase
    {

        [Parameter]
        public AccountStatus Status { get; set; }
        [Parameter]
        public EventCallback OnDataChange { get; set; }
        [Inject]
        public IDialogService DialogService { get; set; }
        [Inject]
        public IAccountStatusService RolesService { get; set; }

        protected string message;
        protected bool isSuccess = false;
        protected bool isFail = false;

        protected async Task RemoveStatus()
        {
            var state = await RolesService.RemoveStatusAsync(Status.Id);

            if (state is true)
            {
                isSuccess = true;
                message = "The role was removed successfully.";
            }
            else
            {
                isFail = true;
                message = "Something went wrong. Couldn't remove the role.";
            }

            await OnDataChange.InvokeAsync(Status.Id);
        }

        protected async Task OpenDeleteDialogAsync()
        {
            var parameters = new DialogParameters
            {
                { "Id", Status.Id },
                { "ContentText", $"Are you sure you want to delete the record with the id {Status.Id} ?" },
                { "ButtonText", "Delete" },
                { "Color", Color.Error }
            };

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog =  DialogService.Show<DialogDeleteWindow>("Delete", parameters, options);
            var result = await dialog.Result;

            if(!result.Cancelled)
            {
                await RemoveStatus();
            }
        }
        protected async Task OpenEditDialogAsync()
        {
            var parameters = new DialogParameters
            {
                { "Status", Status }
            };

            var options = new DialogOptions() { CloseButton=true, MaxWidth = MaxWidth.Small, FullWidth = true };

            var dialog = DialogService.Show<DialogEditStatusWindow>("Edit", parameters, options);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                await OnDataChange.InvokeAsync(Status.Id);
            }
        }

        protected async Task EditStatus()
        {
            var state = await RolesService.EditStatusAsync(Status);

            if(state is true)
            {
                isSuccess = true;
                message = "The status was updated successfully.";
            }
            else
            {
                isFail = true;
                message = "Something went wrong. Couldn't update the status";
            }

            await OnDataChange.InvokeAsync(Status.Id);
        }
    }
}
