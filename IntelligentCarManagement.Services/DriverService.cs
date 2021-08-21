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
        private readonly IUnitOfWork unitOfWork;

        public DriverService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public bool AddDriver(Driver driver)
        {
            var success = true;
            try
            {
                unitOfWork.DriversRepo.Insert(driver);
                unitOfWork.SaveChanges();
            }
            catch(Exception e)
            {
                return !success;
            }

            return success;
        }

        public Driver GetCarDriver(int carID)
        {
            return unitOfWork.DriversRepo.GetCarDriver(carID);
        }

        public async Task<Driver> GetDriver(int id)
        {
            return await unitOfWork.DriversRepo.GetById(id);
        }

        public bool UpdateDriver(Driver driver)
        {
            var success = true;
            try
            {
                unitOfWork.DriversRepo.Update(driver);
                unitOfWork.SaveChanges();
            }
            catch (Exception e)
            {
                return !success;
            }

            return success;
        }
    }
}
