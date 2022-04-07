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
using Api.Services.CustomExceptions;

namespace IntelligentCarManagement.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<UserBase> userManager;

        public UsersService(IUnitOfWork unitOfWork, UserManager<UserBase> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
        }

        public async Task<UserBaseDTO> EditAsync(int id, UserBaseDTO model)
        {
            var user = await userManager.FindByIdAsync(id.ToString());

            if (user is null)
                throw new UserNotFoundException("Couldn't find the user to update.");

            // Map view model to user model
            var config = new MapperConfiguration(cfg => {
                cfg.AddGlobalIgnore("id");
                cfg.CreateMap<UserBaseDTO, UserBase>();
            });

            IMapper iMapper = config.CreateMapper();
            user = iMapper.Map(model, user);

            var result = await userManager.UpdateAsync(user);
            if(!result.Succeeded)
                throw new ItemUpdateException($"Couldn't find the user to update. {result.Errors.FirstOrDefault()}");

            return model;
        }

        public Task<IEnumerable<UserBaseDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        //public async Task<IEnumerable<string>> GetUserRolesAsync(int userId)
        //{
        //    var user = await userManager.FindByIdAsync(userId.ToString());
        //    var userRoles = await userManager.GetRolesAsync(user);

        //    return userRoles;
        //}

        //public void Edit(UserViewModel model)
        //{
        //    unitOfWork.UsersRepo.Update(user);
        //    unitOfWork.SaveChanges();
        //}

        //public async Task<IEnumerable<UserBase>> GetAllUsersAsync()
        //{
        //    return await unitOfWork.UsersRepo.GetAll();
        //}

        public async Task<UserBaseDTO> GetAsync(int id)
        {
            var userToReturn = new UserBaseDTO();
            var user = await userManager.FindByIdAsync(id.ToString());

            // Map user properties to the viewmodel
            // Map view model to user model
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<UserBase, UserBaseDTO>();
            });

            IMapper iMapper = config.CreateMapper();
            userToReturn = iMapper.Map<UserBase, UserBaseDTO>(user);

            return userToReturn;
        }

        public async Task<UserBaseDTO> GetAsync(string email)
        {
            var userToReturn = new UserBaseDTO();
            var user = await userManager.FindByEmailAsync(email);

            // Map user properties to the viewmodel
            // Map view model to user model
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<UserBase, UserBaseDTO>();
            });

            IMapper iMapper = config.CreateMapper();
            userToReturn = iMapper.Map<UserBase, UserBaseDTO>(user);

            return userToReturn;
        }

        public Task RemoveAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveUserAsync(int userId)
        {
            await unitOfWork.UsersRepo.Delete(userId);
            unitOfWork.SaveChanges();
        }
    }
}
