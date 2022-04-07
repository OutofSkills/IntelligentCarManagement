using Api.Services.CustomExceptions;
using Api.Services.Interfaces;
using AutoMapper;
using IntelligentCarManagement.Services;
using Microsoft.AspNetCore.Identity;
using Models;
using Models.Data_Transfer_Objects;
using Models.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.Implementations
{
    public class DriversAccountService : IDriversAccountService
    {
        private readonly ITokenBuilder tokenService;
        private readonly UserManager<UserBase> userManager;

        public DriversAccountService(ITokenBuilder tokenService, UserManager<UserBase> userManager)
        {
            this.tokenService = tokenService;
            this.userManager = userManager;
        }

        public async Task ChangePassword(ResetPasswordDTO model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user is null)
                throw new UserNotFoundException("Couldn't find the account related to the given email.");

            var result = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.Password);

            if (!result.Succeeded) { throw new Exception("Couldn't change the password."); }
        }

        public async Task<string> Login(LoginModel model)
        {
            if (!await tokenService.IsValidUsernameAndPassword(model.Email, model.Password))
            {
                throw new InvalidCredentialsException("Invalid credentials.");
            }

            string token = await tokenService.GenerateToken(model.Email);
            return token;
        }

        public async Task Register(DriverRegisterModel model)
        {
            Driver newDriver = new();

            // Map view model to user model
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<DriverRegisterModel, Driver>();
            });

            IMapper iMapper = config.CreateMapper();
            newDriver = iMapper.Map<DriverRegisterModel, Driver>(model);

            var isEmailValid = await userManager.FindByEmailAsync(model.Email);
            if (isEmailValid is not null)
            {
                throw new Exception("A user with this email already exists.");
            }

            try
            {
                // Creating the new user
                IdentityResult result = await userManager.CreateAsync(newDriver, model.Password);
                if (result.Succeeded is false) { throw new Exception("Something went wrong, please try again."); }
            }
            catch(Exception ex)
            {
                throw new ServerException("Server error, please try again.");
            }

            // Assigning the user to a default role
            var roleName = "DRIVER";
            await userManager.AddToRoleAsync(newDriver, roleName);
        }

        public async Task Remove(int id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            if (user is null)
                return;

            var result = await userManager.DeleteAsync(user);
            if (result.Succeeded is false)
            {
                throw new Exception("Couldn't delete the user with given id.");
            }
        }
    }
}
