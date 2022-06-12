using IntelligentCarManagement.DataAccess.UnitsOfWork;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Api.Services
{
    public class StatusesService : IStatusesService
    {
        private readonly IUnitOfWork unitOfWork;

        public StatusesService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        public void AddAccountStatus(AccountStatus status)
        {
            unitOfWork.StatusesRepo.Insert(status);
            unitOfWork.SaveChanges();
        }

        public void AddRideStatus(RideState status)
        {
            unitOfWork.RideStatesRepo.Insert(status);
            unitOfWork.SaveChanges();
        }

        public void AddApplicationStatus(ApplicationStatus status)
        {
            unitOfWork.ApplicationStatusesRepo.Insert(status);
            unitOfWork.SaveChanges();
        }

        public void EditAccountStatus(AccountStatus status)
        {
            unitOfWork.StatusesRepo.Update(status);
            unitOfWork.SaveChanges();
        }

        public async Task<IEnumerable<AccountStatus>> GetAccountStatusesAsync()
        {
            return await unitOfWork.StatusesRepo.GetAll();
        }

        public async Task<IEnumerable<ApplicationStatus>> GetApplicationStatusesAsync()
        {
            return await unitOfWork.ApplicationStatusesRepo.GetAll();
        }

        public async Task<IEnumerable<RideState>> GetRideStatusesAsync()
        {
            return await unitOfWork.RideStatesRepo.GetAll();
        }

        public async Task RemoveAccountStatusAsync(int id)
        {
            await unitOfWork.StatusesRepo.Delete(id);
            unitOfWork.SaveChanges();
        }
    }
}
