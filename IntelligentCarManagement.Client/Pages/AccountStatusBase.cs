using IntelligentCarManagement.Client.Services;
using IntelligentCarManagement.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Client.Pages
{
    public class AccountStatusBase: ComponentBase
    {
        [Inject]
        public IAccountStatusService AccountStatusService { get; set; }
        [Inject]
        public IDialogService DialogService { get; set; }
        public IEnumerable<AccountStatus> Statuses { get; set; }

        public int NumberOfPages { get; set; }
        public int CurrentPage { get; set; } = 1;


        protected override async Task OnInitializedAsync()
        {
            await LoadStatuses();
        }

        protected async Task DataChanged()
        {
            await LoadStatuses();
        }

        private async Task LoadStatuses(int page = 1)
        {
            var httpResponse = await AccountStatusService.GetStatusesAsync(page);

            NumberOfPages = int.Parse(httpResponse.Headers.GetValues("numberOfPages").FirstOrDefault());

            var responseString = await httpResponse.Content.ReadAsStringAsync();

            Statuses = JsonSerializer.Deserialize<IEnumerable<AccountStatus>>(responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task SelectedPage(int page)
        {
            CurrentPage = page;
            await LoadStatuses(page);
        }

        protected async Task OpenAddDialogAsync()
        {
            var parameters = new DialogParameters();

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true };

            var dialog = DialogService.Show<NewStatus>("New Status", parameters, options);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                await DataChanged();
            }
        }
    }
}
