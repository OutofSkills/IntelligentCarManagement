using IntelligentCarManagement.Client.Services;
using IntelligentCarManagement.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Client.Pages
{
    public class UsersTableRecordBase: ComponentBase
    {
        [Inject]
        public IUsersService UsersService { get; private set; }
        [Parameter]
        public EventCallback OnUserAccountRemoved { get; set; }
        [Parameter]
        public User User { get; set; }

        protected async Task DeleteUser()
        {
            var result = await UsersService.DeleteAccountAsync(User.Id);
            await OnUserAccountRemoved.InvokeAsync(User.Id);
        }
    }
}
