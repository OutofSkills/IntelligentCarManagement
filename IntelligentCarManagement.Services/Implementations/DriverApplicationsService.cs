using Api.Services.CustomExceptions;
using Api.Services.Interfaces;
using Api.Services.Utils;
using Api.Services.Utils.Email_Templates;
using AutoMapper;
using IntelligentCarManagement.DataAccess.UnitsOfWork;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IDriversAccountService driversAccountService;
        private readonly IMailService mailService;
        private readonly IWebHostEnvironment environment;

        public DriverApplicationsService(IUnitOfWork unitOfWork, IDriversAccountService driversAccountService, IMailService mailService, IWebHostEnvironment environment)
        {
            this.unitOfWork = unitOfWork;
            this.driversAccountService = driversAccountService;
            this.mailService = mailService;
            this.environment = environment;
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

            mailService.SendEmail(
                new EmailDTO()
                {
                    SendTo = model.Email,
                    Title = "Driver Application",
                    Action = "driver registration",
                    Content = ""
                },
                new WelcomeTemplate(environment)
                );
        }

        public async Task Approve(int id)
        {
            var application = await unitOfWork.ApplicationsRepo.GetById(id);
            if (application == null)
                throw new NotFoundException("No application with the given id was found.");

            if (application.ApplicationStatus.Name == "APPROVED")
                throw new Exception("This application was already approved.");

            var approvedStatus = await unitOfWork.ApplicationStatusesRepo.GetById(2);
            if (approvedStatus == null)
                throw new NotFoundException("The APPROVED application status was not found.");

            application.ApplicationStatus = approvedStatus;
            unitOfWork.ApplicationsRepo.Update(application);
            unitOfWork.SaveChanges();

            // Create a driver account
            var accountPassword = await driversAccountService.CreateDriver(application);

            mailService.SendEmail(
               new EmailDTO()
               {
                   SendTo = application.Email,
                   Title = "Application Approval",
                   Action = "driver registration",
                   Content = "We are happy to anounce that your application has been approved. <br>" +
                   "Your in app credentials are: <br>" +
                   "<b>Email: </b>" + application.Email +
                   "<b>Password: </b>" + accountPassword
               },
               new ApplicationAcceptedTemplate(environment)
               );
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
