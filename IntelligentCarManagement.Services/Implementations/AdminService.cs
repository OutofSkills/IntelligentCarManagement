using Api.Services.Interfaces;
using AutoMapper;
using IntelligentCarManagement.DataAccess.UnitsOfWork;
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
        private readonly IUnitOfWork unitOfWork;

        public AdminsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<UserBaseDTO> Get(int id)
        {
            var user = await unitOfWork.UsersRepo.GetById(id);

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<UserBase, UserBaseDTO>();
            });

            IMapper iMapper = config.CreateMapper();

            return iMapper.Map<UserBase, UserBaseDTO>(user);
        }
    }
}
