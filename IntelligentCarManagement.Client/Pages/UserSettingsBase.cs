using IntelligentCarManagement.Client.Services;
using IntelligentCarManagement.Models;
using IntelligentCarManagement.Models.NotMapped_Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Client.Pages
{
    public class UserSettingsBase: ComponentBase
    {
        [Parameter]
        public string Id { get; set; }
        public User User { get; set; } = new();
        public ResetPasswordModel ResetPasswordModel { get; set; } = new();
        [Inject]
        public IUsersService UsersService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var httpResponse = await UsersService.GetUserAsync(int.Parse(Id));

            var responseString = await httpResponse.Content.ReadAsStringAsync();

            User = JsonSerializer.Deserialize<User>(responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            ResetPasswordModel.Email = User.Email;
        }
        protected async Task LoadFile(InputFileChangeEventArgs e)
        {
            var format = "iamge/png";
            var resizedImage = await e.File.RequestImageFileAsync(format, 300, 300);
            var buffer = new byte[resizedImage.Size];

            await resizedImage.OpenReadStream().ReadAsync(buffer);
            var imageData = $"data:{format};base64,{Convert.ToBase64String(buffer)}";

            User.Avatar = imageData;
        }
    }
}
