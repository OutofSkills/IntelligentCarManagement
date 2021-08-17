using IntelligentCarManagement.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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

        public bool ChangeAccountStatus(User user, string status)
        {
            // Fetch the wanted status
            // Set it to the user instance
            bool result = true;

            return result;
        }

        public async Task<bool> DeleteAccountAsync(int userId)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("userId", userId.ToString())
            });

            var response = await httpClient.PostAsync($"/api/Users/removeAccount", data);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Edit(User user)
        {
            var response = await httpClient.PostAsJsonAsync("/api/Users/edit", user);

            return response.IsSuccessStatusCode;
        }

        public async Task<HttpResponseMessage> GetUserAsync(int userId)
        {
            return await httpClient.GetAsync($"/api/Users/GetUser?userId={userId}");
        }

        public async Task<HttpResponseMessage> GetUsersAsync(int page = 1, int recordsPerPage = 10)
        {
            var response = await httpClient.GetAsync($"/api/Users/GetUsers?page={page}&recordsPerPage={recordsPerPage}");
            return response;
        }

        public bool NewUser(User user)
        {
            return true;
        }
    }
}
