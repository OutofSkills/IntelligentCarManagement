using IntelligentCarManagement.Client.Services;
using IntelligentCarManagement.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Client.Pages.Drivers
{
    public class DriverHomeBase: ComponentBase
    {
        protected User User { get; set; } = new();

        [Inject] public AuthenticationStateProvider AuthStateProvider { get; set; }

        [Inject] public IDriverService DriversService { get; set; }

        [Inject] public IUsersService UsersService { get; set; }


        protected override async Task OnInitializedAsync()
        {
            var loggedUserId = await GetLoggedUserId();

            var httpResponse = await UsersService.GetUserAsync(loggedUserId);
            var responseString = await httpResponse.Content.ReadAsStringAsync();

            User = JsonSerializer.Deserialize<User>(responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        private async Task<int> GetLoggedUserId()
        {
            var authState = await AuthStateProvider.GetAuthenticationStateAsync();
            var userId = authState.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            return int.Parse(userId);
        }
    }
}
