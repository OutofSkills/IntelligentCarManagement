using Api.Services.CustomExceptions;
using Api.Services.Interfaces;
using Api.Services.Utils;
using AutoMapper;
using IntelligentCarManagement.Services;
using Microsoft.AspNetCore.Identity;
using Models;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.Implementations
{
    public class ClientsAccountService : IClientsAccountService
    {
        private readonly ITokenBuilder tokenService;
        private readonly UserManager<UserBase> _userManager;

        public ClientsAccountService(ITokenBuilder tokenService, UserManager<UserBase> userManager)
        {
            this.tokenService = tokenService;
            this._userManager = userManager;
        }

        public async Task<ResetPasswordDTO> ChangePassword(ResetPasswordDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null)
                throw new NotFoundException("Couldn't find the account related to the given email.");

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.Password);

            if (!result.Succeeded) { throw new Exception("Couldn't change the password."); }

            return model;
        }

        public async Task<LoginResponse> Login(LoginModel model)
        {
            if (!await IsValidUsernameAndPassword(model.Email, model.Password, RoleName.CLIENT))
            {
                throw new InvalidCredentialsException("Invalid credentials.");
            }
            var client = await _userManager.FindByEmailAsync(model.Email);

            string jwtToken = await tokenService.BuildAsync(model.Email);
            string firebaseToken = client.NotificationsToken;
            return new LoginResponse() { FirebaseToken = firebaseToken, JwtToken = jwtToken};
        }

        public async Task Register(ClientRegisterModel model)
        {
            Client newUser = new();

            // Compress user avatar
            model.Avatar = FileCompressor.Compress(model.Avatar);

            // Map view model to user model
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ClientRegisterModel, Client>();
            });

            IMapper iMapper = config.CreateMapper();
            newUser = iMapper.Map<ClientRegisterModel, Client>(model);

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
                await _userManager.AddToRoleAsync(newUser, RoleName.CLIENT.ToString());
            }
            catch(Exception ex)
            {

            }
        }

        public async Task Remove(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user is null)
                return;

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded is false)
            {
                throw new Exception("Couldn't delete the user with given id.");
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
