using IntelligentCarManagement.Client.Services;
using IntelligentCarManagement.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Client.Pages
{
    public class RolesTableRecordBase: ComponentBase
    {
        [Parameter]
        public Role Role { get; set; }
        [Parameter]
        public EventCallback OnDataChange { get; set; }
        [Inject]
        public IRolesService RolesService { get; set; }

        private string message;
        private bool isSuccess = false;
        private bool isFail = false;

        protected async Task RemoveRole()
        {
            var state = await RolesService.RemoveRoleAsync(Role.Id);

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

            await OnDataChange.InvokeAsync(Role.Id);
        }
        protected async Task EditRole()
        {
            await RolesService.EditRoleAsync(Role);
            await OnDataChange.InvokeAsync(Role.Id);
        }
    }
}
