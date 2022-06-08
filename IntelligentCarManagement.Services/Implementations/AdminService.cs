using Api.Services.Interfaces;
using Api.Services.Utils;
using AutoMapper;
using IntelligentCarManagement.DataAccess.UnitsOfWork;
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
    public class AdminsService : IAdminsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<UserBase> _userManager;

        public AdminsService(IUnitOfWork unitOfWork, UserManager<UserBase> userManager)
        {
            this._unitOfWork = unitOfWork;
            this._userManager = userManager;
        }

        public async Task<UserBaseDTO> Get(int id)
        {
            var user = await _unitOfWork.UsersRepo.GetById(id);

            // Compress user avatar
            user.Avatar = FileCompressor.Decompress(user.Avatar);

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<UserBase, UserBaseDTO>();
            });

            IMapper iMapper = config.CreateMapper();

            return iMapper.Map<UserBase, UserBaseDTO>(user);
        }

        public async Task<IEnumerable<UserBaseDTO>> GetAll()
        {
            var users = await _unitOfWork.UsersRepo.GetAll();

            var result = new List<UserBaseDTO>();

            // Map the driver model to the driver DTO
            // Map view model to user model
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<UserBase, UserBaseDTO>();
            });

            IMapper iMapper = config.CreateMapper();

            foreach (var user in users)
            {
                if(await _userManager.IsInRoleAsync(user, RoleName.ADMIN.ToString()))
                {
                    var userObj = iMapper.Map<UserBase, UserBaseDTO>(user);
                    // Decompress user avatar
                    userObj.Avatar = FileCompressor.Decompress(userObj.Avatar);

                    result.Add(userObj);
                }
            }

            return result;
        }
    }
}
