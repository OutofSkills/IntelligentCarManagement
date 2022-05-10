using Api.Services.CustomExceptions;
using Api.Services.Interfaces;
using Api.Services.Utils;
using IntelligentCarManagement.Services;
using Microsoft.AspNetCore.Identity;
using Models;
using Models.Generics;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.Implementations
{
    public class AdminAccountService : IAdminAccountService
    {
        private readonly UserManager<UserBase> _userManager;
        private readonly ITokenBuilder _tokenBuilder;

        public AdminAccountService(UserManager<UserBase> userManager, ITokenBuilder tokenBuilder)
        {
            _userManager = userManager;
            _tokenBuilder = tokenBuilder;
        }

        public async Task<LoginResponse> Login(LoginModel model)
        {
            if (!await IsValidUsernameAndPassword(model.Email, model.Password, RoleName.ADMIN))
            {
                throw new InvalidCredentialsException("Invalid credentials.");
            }
            var client = await _userManager.FindByEmailAsync(model.Email);

            string jwtToken = await _tokenBuilder.BuildAsync(model.Email);
            string firebaseToken = client.NotificationsToken;
            return new LoginResponse() { FirebaseToken = firebaseToken, JwtToken = jwtToken };
        }

        private async Task<bool> IsValidUsernameAndPassword(string username, string password, RoleName role)
        {
            UserBase user = await _userManager.FindByEmailAsync(username);

            // If the user does not exist, it doesn't have the required role or the password is wrong, then return false
            if (user is null)
                return false;

            if (!await _userManager.IsInRoleAsync(user, role.ToString()))
                return false;

            var result = await _userManager.CheckPasswordAsync(user, password);

            return result;
        }
    }
}
