using IntelligentCarManagement.DataAccess.UnitsOfWork;
using IntelligentCarManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Api.Services
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

        public Driver GetDriver(int id)
        {
            return repository.DriversRepo.GetById(id);
        }
    }
}
