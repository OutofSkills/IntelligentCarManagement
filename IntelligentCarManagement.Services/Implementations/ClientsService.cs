using AutoMapper;
using IntelligentCarManagement.DataAccess.UnitsOfWork;
using Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.DTOs;
using Api.Services.CustomExceptions;
using Models.DTOs;
using Api.Services.Utils;

namespace IntelligentCarManagement.Services
{
    public class ClientsService : IClientsService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<UserBase> userManager;

        public ClientsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ClientDTO Add(ClientDTO client)
        {
            throw new NotImplementedException();
        }

        public async Task<ClientDTO> GetAsync(int id)
        {
            var client = await unitOfWork.ClientsRepo.GetById(id);
            // Compress user avatar
            client.Avatar = FileCompressor.Decompress(client.Avatar);

            // Map the driver model to the driver DTO
            // Map view model to user model
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Client, ClientDTO>();
            });

            IMapper iMapper = config.CreateMapper();
            var dto = iMapper.Map<Client, ClientDTO>(client);

            // Get client's rating
            dto.Rating = RatingCalculator.CalculateRating(client);


            return dto;
        }

        public ClientDTO Get(String email)
        {
            var client = unitOfWork.ClientsRepo.GetByEmail(email);
            // Compress user avatar
            client.Avatar = FileCompressor.Decompress(client.Avatar);

            // Map the driver model to the driver DTO
            // Map view model to user model
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Client, ClientDTO>();
            });

            IMapper iMapper = config.CreateMapper();
            var dto = iMapper.Map<Client, ClientDTO>(client);

            // Get client's rating
            dto.Rating = RatingCalculator.CalculateRating(client);

            return dto;
        }

        public async Task<IEnumerable<ClientDTO>> GetAllAsync()
        {
            var clients = await unitOfWork.ClientsRepo.GetAll();

            var result = new List<ClientDTO>();

            // Map the driver model to the driver DTO
            // Map view model to user model
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Client, ClientDTO>();
            });

            IMapper iMapper = config.CreateMapper();

            foreach (var client in clients)
            {
                var clientObj = iMapper.Map<Client, ClientDTO>(client);
                // Decompress user avatar
                clientObj.Avatar = FileCompressor.Decompress(clientObj.Avatar);

                // Get client's rating
                clientObj.Rating = RatingCalculator.CalculateRating(client);

                result.Add(clientObj);
            }

            return result;
        }

        public async Task<ClientDTO> UpdateAsync(int id, ClientDTO clientDTO)
        {
            Client client = await unitOfWork.ClientsRepo.GetById(id);
            if (client == null)
                throw new NotFoundException("No client found with the given id.");

            // Map the driver model to the driver DTO
            var config = new MapperConfiguration(cfg => {
                cfg.AddGlobalIgnore("id");
                cfg.CreateMap<ClientDTO, Client>();
            });

            IMapper iMapper = config.CreateMapper();
            client = iMapper.Map(clientDTO, client);

            // Compress user avatar
            client.Avatar = FileCompressor.Compress(client.Avatar);

            unitOfWork.ClientsRepo.Update(client);
            unitOfWork.SaveChanges();

            return clientDTO;
        }
    }
}
