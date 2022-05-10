using Blazored.LocalStorage;
using Models;
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
using Models.DTOs;

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

        public async Task Login(LoginModel userForAtuthetication)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("email", userForAtuthetication.Email),
                new KeyValuePair<string, string>("password", userForAtuthetication.Password)
            });

            var authResult = await httpClient.PostAsync("http://localhost:41427/api/token", data);
            var authContent = await authResult.Content.ReadAsStringAsync();

            if (authResult.IsSuccessStatusCode == false)
            {
                return;
            }

            var result = JsonSerializer.Deserialize<string>(authContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            await localStorage.SetItemAsync("authToken", result);

            ((AuthStateProvider)authStateProvider).NotifyUserAuthentication(result);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result);

            return;
        }

        public async Task Logout()
        {
            await localStorage.RemoveItemAsync("authToken");
            ((AuthStateProvider)authStateProvider).NotifyUserLogout();
            httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<string> Register(ClientRegisterModel model)
        {
            var registerResult = await httpClient.PostAsJsonAsync("http://localhost:41427/api/Users/register", model);

            if(registerResult.IsSuccessStatusCode is false)
            {
                return "Something went wrong";
            }

            return "Success";
        }

        public async Task<bool> ChangePasswordAsync(ResetPasswordDTO resetPasswordModel)
        {
            var result = await httpClient.PostAsJsonAsync("http://localhost:41427/api/Users/change-password", resetPasswordModel);

            return result.IsSuccessStatusCode;
        }
    }
}
