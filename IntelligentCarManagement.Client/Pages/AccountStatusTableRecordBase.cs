using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntelligentCarManagement.Models;
using IntelligentCarManagement.Client.Services;

namespace IntelligentCarManagement.Client.Pages
{
    public class AccountStatusTableRecordBase: ComponentBase
    {

        [Parameter]
        public AccountStatus Status { get; set; }
        [Parameter]
        public EventCallback OnDataChange { get; set; }
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
