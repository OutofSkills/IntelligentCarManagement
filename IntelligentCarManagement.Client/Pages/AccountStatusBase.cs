using IntelligentCarManagement.Client.Services;
using IntelligentCarManagement.Models;
using Microsoft.AspNetCore.Components;
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
    }
}
