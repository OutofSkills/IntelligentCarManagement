using IntelligentCarManagement.DataAccess.UnitsOfWork;
using IntelligentCarManagement.Models;
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


        public bool AddStatus(AccountStatus status)
        {
            var success = true;

            try
            {
                unitOfWork.StatusesRepo.Insert(status);
                unitOfWork.SaveChanges();
            }catch(Exception e)
            {
                // Do something with the exception
                return false;
            }

            return success;
        }

        public bool EditStatusAsync(AccountStatus status)
        {
            var success = true;

            try
            {
                unitOfWork.StatusesRepo.Update(status);
                unitOfWork.SaveChanges();
            }
            catch (Exception e)
            {
                // Do something with the exception
                return false;
            }

            return success;
        }

        public async Task<IEnumerable<AccountStatus>> GetAllStatusesAsync()
        {
            return await unitOfWork.StatusesRepo.GetAll();
        }

        public async Task<AccountStatus> GetStatusAsync(int id)
        {
            return await unitOfWork.StatusesRepo.GetById(id);
        }

        public async Task<bool> RemoveStatusAsync(int id)
        {
            var success = true;

            try
            {
                await unitOfWork.StatusesRepo.Delete(id);
                unitOfWork.SaveChanges();
            }
            catch (Exception e)
            {
                // Do something with the exception
                return false;
            }

            return success;
        }
    }
}
