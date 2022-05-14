using IntelligentCarManagement.DataAccess.UnitsOfWork;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Api.Services
{
    public class DriverStatusService : IDriverStatusService
    {
        private readonly IUnitOfWork unitOfWork;

        public DriverStatusService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<ApplicationStatus> GetStatusByIdAsync(string id)
        {
            return await unitOfWork.DriverStatusesRepo.GetById(id);
        }

        public ApplicationStatus GetStatusByName(string name)
        {
            return unitOfWork.DriverStatusesRepo.GetByName(name);
        }
    }
}
