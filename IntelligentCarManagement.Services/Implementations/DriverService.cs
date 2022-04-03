using IntelligentCarManagement.DataAccess.UnitsOfWork;
using Models;
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

        public void AddDriver(Driver driver)
        {
            var driverStatus = unitOfWork.DriverStatusesRepo.GetByName("Waiting-Confirmation".ToUpper());
            driver.Status = driverStatus;

            unitOfWork.DriversRepo.Insert(driver);
            unitOfWork.SaveChanges();
        }

        public Driver GetCarDriver(int carID)
        {
            return unitOfWork.DriversRepo.GetCarDriver(carID);
        }

        public async Task<Driver> GetDriver(int id)
        {
            return await unitOfWork.DriversRepo.GetById(id);
        }

        public async Task<IEnumerable<Driver>> GetDrivers()
        {
            return await unitOfWork.DriversRepo.GetAll();
        }

        public void UpdateDriver(Driver driver)
        {
            unitOfWork.DriversRepo.Update(driver);
            unitOfWork.SaveChanges();
        }

        public async Task ChangeDriverStatusAsync(int driverId, string statusName)
        {
            var driver = await unitOfWork.DriversRepo.GetById(driverId);
            var status = unitOfWork.DriverStatusesRepo.GetByName(statusName);

            driver.Status = status;

            unitOfWork.DriversRepo.Update(driver);
            unitOfWork.SaveChanges();
        }
    }
}
