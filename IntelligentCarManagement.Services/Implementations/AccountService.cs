using Api.Services.CustomExceptions;
using Api.Services.Interfaces;
using AutoMapper;
using IntelligentCarManagement.DataAccess.UnitsOfWork;
using IntelligentCarManagement.Services;
using Microsoft.AspNetCore.Identity;
using Models;
using Models.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly ITokenBuilder tokenService;
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<UserBase> userManager;
        private readonly RoleManager<Role> roleManager;

        public AccountService(ITokenBuilder tokenService, IUnitOfWork unitOfWork, UserManager<UserBase> userManager, RoleManager<Role> roleManager)
        {
            this.tokenService = tokenService;
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task ChangePassword(ResetPasswordModel model)
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

            return await tokenService.GenerateToken(model.Email);
        }

        public async Task Register(RegisterModel model)
        {
            UserBase newUser = new();

            // Map view model to user model
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<RegisterModel, UserBase>();
            });

            IMapper iMapper = config.CreateMapper();
            newUser = iMapper.Map<RegisterModel, UserBase>(model);

            var isEmailValid = await userManager.FindByEmailAsync(model.Email);
            if (isEmailValid is not null)
            {
                throw new Exception("A user with this email already exists.");
            }

            // Creating the new user
            IdentityResult result = await userManager.CreateAsync(newUser, model.Password);
            if (result.Succeeded is false) { throw new Exception("Something went wrong, please try again."); }

            // Assigning the user to a default role
            var roleName = "USER";
            await userManager.AddToRoleAsync(newUser, roleName);
        }

        public async Task Remove(int id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            if (user is null)
                return;

            var result = await userManager.DeleteAsync(user);
            if(result.Succeeded is false)
            {
                throw new Exception("Couldn't delete the user with given id.");
            }
        }
    }
}
