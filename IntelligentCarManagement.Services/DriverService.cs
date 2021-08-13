using IntelligentCarManagement.DataAccess.UnitsOfWork;
using IntelligentCarManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Services
{
    public class DriverService : IDriverService
    {
        private readonly IUnitOfWork repository;

        public DriverService(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public Driver GetCarDriver(int carID)
        {
            return repository.DriversRepo.GetCarDriver(carID);
        }

        public async Task<Driver> GetDriver(int id)
        {
            return await repository.DriversRepo.GetById(id);
        }
    }
}
