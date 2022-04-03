using IntelligentCarManagement.Client.Services;
using IntelligentCarManagement.Client.Shared;
using Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Client.Pages.Management.Users
{
    public class UsersTableRecordBase: ComponentBase
    {
        [Inject] public IUsersService UsersService { get; private set; }

        [Inject] public IDialogService DialogService { get; set; }

        [Inject] public ISnackbar Snackbar { get; set; }

        [Parameter] public EventCallback OnUserAccountRemoved { get; set; }

        [Parameter] public UserBase User { get; set; }

        protected async Task OpenDeleteDialogAsync()
        {
            var parameters = new DialogParameters
            {
                { "Id", User.Id },
                { "ContentText", $"Are you sure you want to delete the record with the id {User.Id} ?" },
                { "ButtonText", "Delete" },
                { "Color", Color.Error }
            };

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = DialogService.Show<DialogDeleteWindow>("Delete", parameters, options);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                await DeleteUser();
            }
        }

        protected async Task DeleteUser()
        {
            var state = await UsersService.DeleteAccountAsync(User.Id);

            if (state is true)
            {
                Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomCenter;
                Snackbar.Add("User deleted successfully.", Severity.Success);
            }
            else
            {
                Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomCenter;
                Snackbar.Add("Something went wrong. Couldn't remove the user.", Severity.Error);
            }

            await OnUserAccountRemoved.InvokeAsync(User.Id);
        }
    }
}
