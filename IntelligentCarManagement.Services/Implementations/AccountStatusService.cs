using IntelligentCarManagement.DataAccess.UnitsOfWork;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Api.Services
{
    public class AccountStatusService : IAccountStatusService
    {
        private readonly IUnitOfWork unitOfWork;

        public AccountStatusService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        public void AddStatus(AccountStatus status)
        {
            unitOfWork.StatusesRepo.Insert(status);
            unitOfWork.SaveChanges();
        }

        public void EditStatus(AccountStatus status)
        {
            unitOfWork.StatusesRepo.Update(status);
            unitOfWork.SaveChanges();
        }

        public async Task<IEnumerable<AccountStatus>> GetAllStatusesAsync()
        {
            return await unitOfWork.StatusesRepo.GetAll();
        }

        public async Task<AccountStatus> GetStatusAsync(int id)
        {
            return await unitOfWork.StatusesRepo.GetById(id);
        }

        public async Task RemoveStatusAsync(int id)
        {
            await unitOfWork.StatusesRepo.Delete(id);
            unitOfWork.SaveChanges();
        }
    }
}
