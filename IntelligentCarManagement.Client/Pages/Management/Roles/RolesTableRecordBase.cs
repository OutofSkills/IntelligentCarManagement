using IntelligentCarManagement.Client.Services;
using IntelligentCarManagement.Client.Shared;
using IntelligentCarManagement.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Client.Pages.Management.Roles
{
    public class RolesTableRecordBase: ComponentBase
    {
        [Parameter]
        public Role Role { get; set; }
        [Parameter]
        public EventCallback OnDataChange { get; set; }
        [Inject] public IRolesService RolesService { get; set; }
        [Inject] IDialogService DialogService { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }

        protected async Task RemoveRole()
        {
            var state = await RolesService.RemoveRoleAsync(Role.Id);

            if (state is true)
            {
                Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomCenter;
                Snackbar.Add("Role deleted successfully.", Severity.Success);
            }
            else
            {
                Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomCenter;
                Snackbar.Add("Something went wrong. Couldn't remove the role.", Severity.Error);
            }

            await OnDataChange.InvokeAsync(Role.Id);
        }
        protected async Task EditRole()
        {
            await RolesService.EditRoleAsync(Role);
            await OnDataChange.InvokeAsync(Role.Id);
        }

        protected async Task OpenDeleteDialogAsync()
        {
            var parameters = new DialogParameters
            {
                { "Id", Role.Id },
                { "ContentText", $"Are you sure you want to delete the record with the id {Role.Id} ?" },
                { "ButtonText", "Delete" },
                { "Color", Color.Error }
            };

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = DialogService.Show<DialogDeleteWindow>("Delete", parameters, options);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                await RemoveRole();
            }
        }
        protected async Task OpenEditDialogAsync()
        {
            var parameters = new DialogParameters
            {
                { "Role", Role }
            };

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true };

            var dialog = DialogService.Show<DialogEditRoleWindow>("Edit", parameters, options);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                await OnDataChange.InvokeAsync(Role.Id);
            }
        }
    }
}
