using Api.Services.CustomExceptions;
using Api.Services.Interfaces;
using Api.Services.Utils;
using AutoMapper;
using IntelligentCarManagement.Services;
using Microsoft.AspNetCore.Identity;
using Models;
using Models.DTOs;
using Models.DTOs;
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
        private readonly UserManager<UserBase> _userManager;

        public DriversAccountService(ITokenBuilder tokenService, UserManager<UserBase> userManager)
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
            if (!await IsValidUsernameAndPassword(model.Email, model.Password, RoleName.DRIVER))
            {
                throw new InvalidCredentialsException("Invalid credentials.");
            }

            var driver = await _userManager.FindByEmailAsync(model.Email);

            string jwtToken = await tokenService.BuildAsync(model.Email);
            string firebaseToken = driver.NotificationsToken;
            return new LoginResponse() { FirebaseToken = firebaseToken, JwtToken = jwtToken };
        }

        public async Task<string> CreateDriver(DriverApplication application)
        {
            Driver newDriver = new();

            // Map view model to user model
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<DriverApplication, Driver>()
                .ForMember(x => x.Id, opt => opt.Ignore());
                
            });

            IMapper iMapper = config.CreateMapper();
            newDriver = iMapper.Map<DriverApplication, Driver>(application);

            var isEmailValid = await _userManager.FindByEmailAsync(application.Email);
            if (isEmailValid is not null)
            {
                throw new Exception("A user with this email already exists.");
            }
            string generatedPassword = "";

            try
            {
                generatedPassword = GeneratePassword();
                newDriver.UserName = newDriver.UserName.Trim(); // No white spaces allowed

                // Creating the new user
                IdentityResult result = await _userManager.CreateAsync(newDriver, generatedPassword);
                if (result.Succeeded is false) { return null; }
            }
            catch (Exception ex)
            {
                throw new ServerException("Server error, please try again.");
            }

            // Assigning the user to a default role
            var roleResult = await _userManager.AddToRoleAsync(newDriver, RoleName.DRIVER.ToString());
            if(roleResult.Succeeded is false) { return null; }

            return generatedPassword;
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

        public string GeneratePassword()
        {
            var options = _userManager.Options.Password;

            int length = options.RequiredLength;

            bool digit = options.RequireDigit;
            bool lowercase = options.RequireLowercase;
            bool uppercase = options.RequireUppercase;

            StringBuilder password = new StringBuilder();
            Random random = new Random();

            while (password.Length < length)
            {
                char c = (char)random.Next(32, 126);

                if (char.IsDigit(c))
                {
                    digit = false;
                    password.Append(c);
                }
                else if (char.IsLower(c))
                {
                    lowercase = false;
                    password.Append(c);
                }
                else if (char.IsUpper(c))
                {
                    uppercase = false;
                    password.Append(c);
                }
            }

            if (digit)
                password.Append((char)random.Next(48, 58));
            if (lowercase)
                password.Append((char)random.Next(97, 123));
            if (uppercase)
                password.Append((char)random.Next(65, 91));

            return password.ToString();
        }
    }
}
