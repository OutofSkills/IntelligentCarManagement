using IntelligentCarManagement.Client.Services;
using IntelligentCarManagement.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Client.Pages
{
    public class UsersBase: ComponentBase
    {
        [Inject]
        public IUsersService UsersService { get; set; }

        public IEnumerable<User> Users { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Users = await UsersService.GetUsersAsync();
        }

    }
}

