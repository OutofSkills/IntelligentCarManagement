using Blazored.LocalStorage;
using IntelligentCarManagement.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Text.Json;
using IntelligentCarManagement.Authentication;
using System.Net.Http.Headers;

namespace IntelligentCarManagement.Client.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient httpClient;
        private readonly AuthenticationStateProvider authStateProvider;
        private readonly ILocalStorageService localStorage;

        public AuthenticationService(HttpClient httpClient, AuthenticationStateProvider authStateProvider, ILocalStorageService localStorage)
        {
            this.httpClient = httpClient;
            this.authStateProvider = authStateProvider;
            this.localStorage = localStorage;
        }

        public async Task<User> Login(User userForAtuthetication)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", userForAtuthetication.Email),
                new KeyValuePair<string, string>("password", userForAtuthetication.Password)
            });

            var authResult = await httpClient.PostAsJsonAsync("", data);
            var authContent = await authResult.Content.ReadAsStringAsync();

            if (authResult.IsSuccessStatusCode == false)
            {
                return null;
            }

            var result = JsonSerializer.Deserialize<User>(authContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            await localStorage.SetItemAsync("authToken", result.Access_Token);

            ((AuthStateProvider)authStateProvider).NotifyUserAuthentication(result.Access_Token);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Access_Token);

            return result;
        }

        public async Task Logout()
        {
            await localStorage.RemoveItemAsync("authToken");
            ((AuthStateProvider)authStateProvider).NotifyUserLogout();
            httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
