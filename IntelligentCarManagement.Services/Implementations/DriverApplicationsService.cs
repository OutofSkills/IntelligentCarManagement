using Api.Services.Interfaces;
using Api.Services.Utils;
using AutoMapper;
using IntelligentCarManagement.DataAccess.UnitsOfWork;
using Models;
using Models.Data_Transfer_Objects;
using Models.DTOs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.Implementations
{
    public class DriverApplicationsService : IDriverApplicationsService
    {
        private readonly IUnitOfWork unitOfWork;

        public DriverApplicationsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void Apply(DriverApplicationDTO model)
        {
            // First compress the files
            model.Avatar = FileCompressor.Compress(model.Avatar);
            model.CV = FileCompressor.Compress(model.CV);

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<DriverApplicationDTO, DriverApplication>();
                cfg.CreateMap<AddressDto, UserAddress>();
            });

            IMapper iMapper = config.CreateMapper();

            var application = iMapper.Map<DriverApplicationDTO, DriverApplication>(model);

            unitOfWork.ApplicationsRepo.Insert(application);
            unitOfWork.SaveChanges();
        }

        public async Task<IEnumerable<DriverApplicationDTO>> GetAll()
        {
            var applications = await unitOfWork.ApplicationsRepo.GetAll();

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<DriverApplication, DriverApplicationDTO>();
                    cfg.CreateMap<UserAddress, AddressDto>();
            });

            IMapper iMapper = config.CreateMapper();
            var result = new List<DriverApplicationDTO>();

            foreach (var application in applications)
            {
                var item = iMapper.Map<DriverApplication, DriverApplicationDTO>(application);

                // Decompress the files
                item.Avatar = FileCompressor.Decompress(item.Avatar);
                item.CV = FileCompressor.Decompress(item.CV);

                result.Add(item);
            }
            
            return result;
        }

        public async Task<DriverApplicationDTO> GetAsync(int id)
        {
            var application = await unitOfWork.ApplicationsRepo.GetById(id);

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<DriverApplication, DriverApplicationDTO>();
                cfg.CreateMap<UserAddress, AddressDto>();
            });

            IMapper iMapper = config.CreateMapper();
            var item = iMapper.Map<DriverApplication, DriverApplicationDTO>(application);

            // Decompress the files
            item.Avatar = FileCompressor.Decompress(item.Avatar);
            item.CV = FileCompressor.Decompress(item.CV);

            return item;
        }
    }
}
