using IntelligentCarManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Client.Services
{
    public class AccountStatusService : IAccountStatusService
    {
        private readonly HttpClient httpClient;

        public AccountStatusService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<bool> AddStatus(AccountStatus status)
        {
            var result = await httpClient.PostAsJsonAsync("/api/AccountStatus/add-status", status);

            return result.IsSuccessStatusCode;
        }

        public async Task<bool> EditStatusAsync(AccountStatus status)
        {
            var result = await httpClient.PutAsJsonAsync("/api/AccountStatus/edit-status", status);

            return result.IsSuccessStatusCode;
        }

        public async Task<AccountStatus> GetStatus(int id)
        {
            var status = await httpClient.GetFromJsonAsync<AccountStatus>($"/api/AccountStatus/add-status?id={id}");

            return status;
        }

        public async Task<HttpResponseMessage> GetStatusesAsync(int page = 1, int recordsPerPage = 10)
        {
            var response = await httpClient.GetAsync($"/api/AccountStatus/get-statuses?page={page}&recordsPerPage={recordsPerPage}");

            return response;
        }

        public async Task<IEnumerable<AccountStatus>> GetStatusesAsync()
        {
            var response = await httpClient.GetFromJsonAsync<AccountStatus[]>($"/api/AccountStatus/get-all-statuses");

            return response;
        }

        public async Task<bool> RemoveStatusAsync(int id)
        {
            var result = await httpClient.DeleteAsync($"/api/AccountStatus/remove-status?id={id}");

            return result.IsSuccessStatusCode;
        }
    }
}
