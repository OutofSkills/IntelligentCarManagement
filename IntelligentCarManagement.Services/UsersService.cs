using IntelligentCarManagement.DataAccess.UnitsOfWork;
using IntelligentCarManagement.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;

        public UsersService(IUnitOfWork unitOfWork, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public bool AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public bool EditUser(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await unitOfWork.UsersRepo.GetAll();
        }

        public Task<User> GetUser(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<string> RegisterUser(User userToRegister)
        {
            /*Completing default user data*/
            // Set status to pendind activation
            userToRegister.AccountStatus = await unitOfWork.StatusesRepo.GetById(1);
            userToRegister.RegistrationDate = DateTime.Now;
            /*End completing*/

            User existingUser = await userManager.FindByEmailAsync(userToRegister.Email);

            if(existingUser is not null)
            {
                return "A user with this email already exists.";
            }

            try
            {
                IdentityResult result = await userManager.CreateAsync(userToRegister, userToRegister.Password);

                if(result.Succeeded is false) { return "Something went wrong, please try again."; }

                string defaultRole = "USER";
                var roleExists = await roleManager.RoleExistsAsync(defaultRole);

                if(roleExists is not true)
                {
                    Role role = new()
                    {
                        Name = defaultRole,
                        Description = "Basic user that can see his profile and can choose to be a driver or a client"
                    };

                    var roleCreationResult = await roleManager.CreateAsync(role);

                    if(roleCreationResult.Succeeded is false)
                    {
                        return "Sorry, could not assign to role";
                    }
                }

                await userManager.AddToRoleAsync(userToRegister, defaultRole);

                var roleToAssign = await roleManager.FindByNameAsync(defaultRole);
                userToRegister.UserRoles.Add(new UserRole { Role = roleToAssign, RoleId = roleToAssign.Id, User = userToRegister});
                
                var updateResult = await userManager.UpdateAsync(userToRegister);

                if (updateResult.Succeeded)
                {
                    return "Success";
                }
            }
            catch(Exception e)
            {
                return e.InnerException.Message.ToString();
            }

            return "Fail";
        }

        public bool RemoveUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
