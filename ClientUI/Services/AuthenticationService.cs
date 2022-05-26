using Blazored.LocalStorage;
using Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IntelligentCarManagement.Authentication;
using System.Net.Http.Headers;
using Models.DTOs;
using ClientUI.Utils;
using Newtonsoft.Json;
using System.Text;

namespace ClientUI.Services
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

        public async Task<Utils.RequestResponse> Login(string email, string password)
        {
            var loginModel = new LoginModel() { Email = email, Password = password };
            var json = JsonConvert.SerializeObject(loginModel);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var authResult = await httpClient.PostAsync("https://intellicarsapi.azurewebsites.net/api/AdminAccount/login", stringContent);
            var authContent = await authResult.Content.ReadAsStringAsync();

            if (authResult.IsSuccessStatusCode == false)
            {
                return new Utils.RequestResponse() { IsSuccess = false, Message = authResult.ReasonPhrase};
            }

            var result = System.Text.Json.JsonSerializer.Deserialize<LoginResponse>(authContent, new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            await localStorage.SetItemAsync("authToken", result.JwtToken);

            ((AuthStateProvider)authStateProvider).NotifyUserAuthentication(result.JwtToken);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.JwtToken);

            return new Utils.RequestResponse() { IsSuccess = true, Message = "Successfully logged in." }; 
        }

        public async Task Logout()
        {
            await localStorage.RemoveItemAsync("authToken");
            ((AuthStateProvider)authStateProvider).NotifyUserLogout();
            httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<Utils.RequestResponse> Register(ClientRegisterModel model)
        {
            var json = JsonConvert.SerializeObject(model);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var registerResult = await httpClient.PostAsync("https://intellicarsapi.azurewebsites.net/api/AdminAccount", stringContent);

            if(registerResult.IsSuccessStatusCode is false)
            {
                return new Utils.RequestResponse() { IsSuccess = false, Message = registerResult.ReasonPhrase };
            }

            return new Utils.RequestResponse() { IsSuccess = true, Message = "Successfully signed in." };
        }

        public async Task<Utils.RequestResponse> ChangePasswordAsync(ResetPasswordDTO resetPasswordModel)
        {
            var json = JsonConvert.SerializeObject(resetPasswordModel);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var result = await httpClient.PostAsync("https://intellicarsapi.azurewebsites.net/api/AdminAccount/change-password", stringContent);

            if (result.IsSuccessStatusCode is false)
            {
                return new Utils.RequestResponse() { IsSuccess = false, Message = result.ReasonPhrase };
            }

            return new Utils.RequestResponse() { IsSuccess = true, Message = "Successfully changed password." };
        }
    }
}
