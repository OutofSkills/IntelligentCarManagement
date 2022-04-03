using Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Client.Services
{
    public class UsersService : IUsersService
    {
        private readonly HttpClient httpClient;

        public UsersService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public bool ChangeAccountStatus(UserBase user, string status)
        {
            // Fetch the wanted status
            // Set it to the user instance
            bool result = true;

            return result;
        }

        public async Task<bool> DeleteAccountAsync(int userId)
        {
            var response = await httpClient.DeleteAsync($"/api/Users/remove-account?id={userId}");

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Edit(UserBase user)
        {
            var response = await httpClient.PutAsJsonAsync("/api/Users/edit", user);

            return response.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateUserRoles(UserBase user)
        {
            var response = await httpClient.PostAsJsonAsync("/api/Users/edit-roles", user);

            return response.IsSuccessStatusCode;
        }

        public async Task<HttpResponseMessage> GetUserAsync(int userId)
        {
            return await httpClient.GetAsync($"/api/Users/get-user?userId={userId}");
        }

        public async Task<IEnumerable<string>> GetUserRolesAsync(int userId)
        {
            var httpResponse = await httpClient.GetAsync($"/api/Users/get-user-roles?userId={userId}");
            var responseString = await httpResponse.Content.ReadAsStringAsync();

            var userRoles = JsonSerializer.Deserialize<IEnumerable<string>>(responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return userRoles;
        }

        public async Task<HttpResponseMessage> GetUsersAsync(int page = 1, int recordsPerPage = 10)
        {
            var response = await httpClient.GetAsync($"/api/Users/get-users?page={page}&recordsPerPage={recordsPerPage}");
            return response;
        }
    }
}
