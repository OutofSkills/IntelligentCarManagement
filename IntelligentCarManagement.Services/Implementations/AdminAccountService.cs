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
using AutoMapper;
using Models.Data_Transfer_Objects;

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

        public async Task Register(IRegisterModel model)
        {

            UserBase newUser = new();

            // Compress user avatar
            model.Avatar = FileCompressor.Compress(model.Avatar);

            // Map view model to user model
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<AdminRegisterModel, UserBase>();
            });

            IMapper iMapper = config.CreateMapper();
            newUser = iMapper.Map<AdminRegisterModel, UserBase>((AdminRegisterModel)model);

            var isEmailValid = await _userManager.FindByEmailAsync(model.Email);
            if (isEmailValid is not null)
            {
                throw new Exception("A user with this email already exists.");
            }

            try
            {
                // Creating the new user
                IdentityResult result = await _userManager.CreateAsync(newUser, model.Password);
                if (result.Succeeded is false) { throw new Exception("Something went wrong, please try again."); }

                // Assigning the user to a default role
                await _userManager.AddToRoleAsync(newUser, RoleName.ADMIN.ToString());
            }
            catch (Exception ex)
            {
                throw new ServerException(ex.Message);
            }
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
