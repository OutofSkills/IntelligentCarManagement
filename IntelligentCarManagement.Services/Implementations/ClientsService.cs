﻿using AutoMapper;
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
using Models.Data_Transfer_Objects;

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

            // Map the driver model to the driver DTO
            // Map view model to user model
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Client, ClientDTO>();
            });

            IMapper iMapper = config.CreateMapper();

            return iMapper.Map<Client, ClientDTO>(client);
        }

        public ClientDTO Get(String email)
        {
            var client = unitOfWork.ClientsRepo.GetByEmail(email);

            // Map the driver model to the driver DTO
            // Map view model to user model
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Client, ClientDTO>();
            });

            IMapper iMapper = config.CreateMapper();

            return iMapper.Map<Client, ClientDTO>(client);
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
                result.Add(iMapper.Map<Client, ClientDTO>(client));
            }

            return result;
        }

        public async Task<ClientDTO> UpdateAsync(int id, ClientDTO clientDTO)
        {
            Client client = await unitOfWork.ClientsRepo.GetById(id);
            if (client == null)
                throw new UserNotFoundException("No client found with the given id.");

            // Map the driver model to the driver DTO
            var config = new MapperConfiguration(cfg => {
                cfg.AddGlobalIgnore("id");
                cfg.CreateMap<ClientDTO, Client>();
            });

            IMapper iMapper = config.CreateMapper();
            client = iMapper.Map(clientDTO, client);

            unitOfWork.ClientsRepo.Update(client);
            unitOfWork.SaveChanges();

            return clientDTO;
        }
    }
}