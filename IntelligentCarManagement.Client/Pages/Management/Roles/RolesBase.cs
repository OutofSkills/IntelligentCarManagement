using IntelligentCarManagement.Client.Services;
using Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Client.Pages.Management.Roles
{
    public class RolesBase: ComponentBase
    {
        [Inject]
        public IRolesService RolesService { get; set; }
        [Inject]
        public IDialogService DialogService { get; set; }
        protected IEnumerable<Role> Roles { get; set; }

        public int NumberOfPages { get; set; }
        public int CurrentPage { get; set; } = 1;


        protected override async Task OnInitializedAsync()
        {
            await LoadRoles();
        }

        protected async Task DataChanged()
        {
            await LoadRoles();
        }

        private async Task LoadRoles(int page = 1)
        {
            var httpResponse = await RolesService.GetRolesAsync(page);

            NumberOfPages = int.Parse(httpResponse.Headers.GetValues("numberOfPages").FirstOrDefault());

            var responseString = await httpResponse.Content.ReadAsStringAsync();

            Roles = JsonSerializer.Deserialize<IEnumerable<Role>>(responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task SelectedPage(int page)
        {
            CurrentPage = page;
            await LoadRoles(page);
        }

        protected async Task OpenAddDialogAsync()
        {
            var parameters = new DialogParameters();

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, FullWidth = true };

            var dialog = DialogService.Show<NewRole>("New Role", parameters, options);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                await DataChanged();
            }
        }
    }
}
