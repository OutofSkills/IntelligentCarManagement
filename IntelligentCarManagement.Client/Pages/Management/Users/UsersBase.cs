using IntelligentCarManagement.Client.Services;
using Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace IntelligentCarManagement.Client.Pages.Management.Users
{
    public class UsersBase: ComponentBase
    {
        [Inject]
        public IUsersService UsersService { get; set; }

        public IEnumerable<UserBase> Users { get; set; }

        public int NumberOfPages { get; set; }
        public int CurrentPage { get; set; } = 1;

        protected override async Task OnInitializedAsync()
        {
            await LoadUsers();
        }

        public async Task SelectedPage(int page)
        {
            CurrentPage = page;
            await LoadUsers(page);
        }

        private async Task LoadUsers(int page = 1)
        {
            var httpResponse = await UsersService.GetUsersAsync(page);

            NumberOfPages = int.Parse(httpResponse.Headers.GetValues("numberOfPages").FirstOrDefault());

            var responseString = await httpResponse.Content.ReadAsStringAsync();

            Users = JsonSerializer.Deserialize<IEnumerable<UserBase>>(responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true});
        }

        public async Task UserRemoved()
        {
            await LoadUsers();
        }

    }
}

