using IntelligentCarManagement.Client.Services;
using IntelligentCarManagement.Models;
using IntelligentCarManagement.Models.NotMapped_Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Client.Pages.Management.Users
{
    public class UserSettingsBase: ComponentBase
    {
        [Parameter]
        public string Id { get; set; }
        public User User { get; set; } = new();
        public ResetPasswordModel ResetPasswordModel { get; set; } = new();
        [Inject]
        public IUsersService UsersService { get; set; }
        [Inject]
        public IRolesService RolesService { get; set; }
        public List<RolesCheckBoxModel> UserCheckedRoles { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            var httpResponse = await UsersService.GetUserAsync(int.Parse(Id));

            var responseString = await httpResponse.Content.ReadAsStringAsync();

            User = JsonSerializer.Deserialize<User>(responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            ResetPasswordModel.Email = User.Email;


            await AssignCheckBoxRoles();
        }
        protected override void OnParametersSet()
        {
            if (User.Driver == null)
            {
                User.Driver = new Driver();
                User.Driver.UserId = User.Id;
            }
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

        private async Task AssignCheckBoxRoles()
        {
            var existingRoles = await RolesService.GetRolesAsync();
            var userRoles = await UsersService.GetUserRolesAsync(User.Id);

            foreach(var role in existingRoles)
            {
                var isRoleAssigned = false;

                foreach(var userRole in userRoles)
                {
                    if(role.Name == userRole)
                    {
                        isRoleAssigned = true;
                        break;
                    }
                }
                UserCheckedRoles.Add(new RolesCheckBoxModel() { Role = role, IsAssigned = isRoleAssigned });
            }
               
        }
    }
}
