using AutoMapper;
using IntelligentCarManagement.DataAccess.UnitsOfWork;
using Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.View_Models;

namespace IntelligentCarManagement.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<UserBase> userManager;
        private readonly RoleManager<Role> roleManager;

        public UsersService(IUnitOfWork unitOfWork, UserManager<UserBase> userManager, RoleManager<Role> roleManager)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<IEnumerable<string>> GetUserRolesAsync(int userId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            var userRoles = await userManager.GetRolesAsync(user);

            return userRoles;
        }

        public void EditUser(UserBase user)
        {
            unitOfWork.UsersRepo.Update(user);
            unitOfWork.SaveChanges();
        }

        public async Task<IEnumerable<UserBase>> GetAllUsersAsync()
        {
            return await unitOfWork.UsersRepo.GetAll();
        }

        public async Task<UserBase> GetUserAsync(int id)
        {
            return await unitOfWork.UsersRepo.GetById(id);
        }

        public async Task RegisterUser(RegisterModel model)
        {
            UserBase userToRegister = new();

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<RegisterModel, UserBase>();
            });

            IMapper iMapper = config.CreateMapper();
            userToRegister = iMapper.Map<RegisterModel, UserBase>(model);

            /* Completing default user data */
            // Set status to pendind activation
            userToRegister.AccountStatus = await unitOfWork.StatusesRepo.GetById(1);
            userToRegister.RegistrationDate = DateTime.Now;
            userToRegister.Address = new UserAddress();
            /* End completing */

            if(!await IsValidAsync(userToRegister))
            {
                throw new Exception("A user with this email already exists.");
            }

            IdentityResult result = await userManager.CreateAsync(userToRegister, model.Password);

            if(result.Succeeded is false) { throw new Exception("Something went wrong, please try again."); }

            await AssignToDefaultRole(userToRegister);   

        }


        public async Task ChangePasswordAsync(ResetPasswordModel resetPasswordModel)
        {
            var user = await userManager.FindByEmailAsync(resetPasswordModel.Email);
            var result = await userManager.ChangePasswordAsync(user, resetPasswordModel.CurrentPassword, resetPasswordModel.Password);

            if (!result.Succeeded) { throw new Exception("Couldn't change the password."); }
        }

        public async Task RemoveUserAsync(int userId)
        {
            await unitOfWork.UsersRepo.Delete(userId);
            unitOfWork.SaveChanges();
        }

        public async Task UpdateUserRoles(UserBase user)
        {
            List<string> assignedRolesNames = new();

            foreach (var userRole in user.UserRoles)
            {
                assignedRolesNames.Add(userRole.Role.Name);
            }
                
            var userToUpdate = await userManager.FindByEmailAsync(user.Email);
            var rolesToRemove = await userManager.GetRolesAsync(userToUpdate);

            var removeResult = await userManager.RemoveFromRolesAsync(userToUpdate, rolesToRemove);
            if(!removeResult.Succeeded){ throw new Exception("Couldn't remove old roles."); }

            var result = await userManager.AddToRolesAsync(userToUpdate, assignedRolesNames);
            if (!result.Succeeded) { throw new Exception("Couldn't assign default role."); }
        }

        private async Task<bool> IsValidAsync(UserBase userToRegister)
        {
            UserBase existingUser = await userManager.FindByEmailAsync(userToRegister.Email);

            if (existingUser is null)
            {
                return true;
            }

            return false;
        }

        private async Task AssignToDefaultRole(UserBase userToRegister)
        {
            var defaultRole = "USER";
            var description = "Basic user that can see his profile and can choose to be a driver or a client";

            var roleExists = await roleManager.RoleExistsAsync(defaultRole);

            if (roleExists)
            {
                await userManager.AddToRoleAsync(userToRegister, defaultRole);
                return;
            }

            Role role = new()
            {
                Name = defaultRole,
                Description = description
            };

            var roleCreationResult = await roleManager.CreateAsync(role);

            if (roleCreationResult.Succeeded is false)
            {
                throw new Exception("Sorry, could not assign to role");
            }
        }
    }
}
