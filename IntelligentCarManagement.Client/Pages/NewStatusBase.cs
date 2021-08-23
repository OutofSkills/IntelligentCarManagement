using IntelligentCarManagement.Client.Services;
using IntelligentCarManagement.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Client.Pages
{
    public class NewStatusBase: ComponentBase
    {
        protected AccountStatus Status = new();
        [Inject]
        public IAccountStatusService AccountStatusService { get; set; }
        [Parameter]
        public EventCallback OnDataChanged { get; set; }

        protected string message;
        protected bool isSuccess = false;
        protected bool isFail = false;

        protected async Task AddStatus()
        {
            var state = await AccountStatusService.AddStatus(Status);

            if (state is true)
            {
                isSuccess = true;
                message = "The role was added successfully";
            }
            else
            {
                isFail = true;
                message = "Something went wrong. Couldn't add the role";
            }

            await OnDataChanged.InvokeAsync(Status.Name);
        }
    }
}
