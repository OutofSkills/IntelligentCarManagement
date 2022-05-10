using IntelligentCarManagement.Client.Services;
using Models;
using Models.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Client.Pages
{
    public class SettingsBase: ComponentBase
    {
        [Inject] public AuthenticationStateProvider AuthStateProvider { get; set; }

        [Inject] public IUsersService UsersService { get; set; }

        protected UserBase User { get; set; } = new();

        public ResetPasswordDTO ResetPasswordModel { get; set; } = new();

        protected async override Task OnInitializedAsync()
        {
            var loggedUserId = await GetLoggedUserId();

            var httpResponse = await UsersService.GetUserAsync(loggedUserId);
            var responseString = await httpResponse.Content.ReadAsStringAsync();

            User = JsonSerializer.Deserialize<UserBase>(responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            ResetPasswordModel.Email = User.Email;
        }

        protected async Task LoadFile(InputFileChangeEventArgs e)
        {
            //var format = "iamge/png";
            //var resizedImage = await e.File.RequestImageFileAsync(format, 300, 300);
            //var buffer = new byte[resizedImage.Size];

            //await resizedImage.OpenReadStream().ReadAsync(buffer);
            //var imageData = $"data:{format};base64,{Convert.ToBase64String(buffer)}";

            //User.Avatar = imageData;
        }

        private async Task<int> GetLoggedUserId()
        {
            var authState = await AuthStateProvider.GetAuthenticationStateAsync();
            var userId = authState.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            return int.Parse(userId);
        }
    }
}
