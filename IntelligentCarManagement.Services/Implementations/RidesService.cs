using IntelligentCarManagement.DataAccess.UnitsOfWork;
using Models;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Api.Services
{
    public class RidesService : IRidesService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RidesService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<RideRequestResponse> RequestAsync(Ride ride)
        {
            if (ride is null)
                throw new Exception("No data provided");

            if (ride.PickUpTime == default)
                ride.PickUpTime = DateTime.Now;

            // Check if the driver is available
            var driver = await _unitOfWork.DriversRepo.GetById(ride.DriverId);
            if (driver is null)
                throw new Exception("Driver not found");

            if (!driver.IsAvailable)
                return new RideRequestResponse() { Success = false, Message = $"Driver {driver.FirstName} {driver.LastName} is not available." };

            /* If the driver exists and is available
             * change his availability to not available 
             */
            driver.IsAvailable = false;

            _unitOfWork.DriversRepo.Update(driver);
            _unitOfWork.RidesRepo.Insert(ride);
            _unitOfWork.SaveChanges();

            return new RideRequestResponse { Success = true, Message = $"Driver {driver.FirstName} {driver.LastName} was successfully assigned to your request." };
        }

        public async Task AssignDriverAsync(int rideId, int driverId)
        {
            var ride = await _unitOfWork.RidesRepo.GetById(rideId);
            if (ride is null)
                throw new Exception("Couldn't find information about the ride with the given id");

            var driver = await _unitOfWork.DriversRepo.GetById(driverId);
            if (driver is null)
                throw new Exception("Couldn't find information about the driver with the given id");

            ride.DriverId = driverId;
            _unitOfWork.RidesRepo.Update(ride);
            _unitOfWork.SaveChanges();

        }

        public void Update(int id, Ride ride)
        {
            _unitOfWork.RidesRepo.Update(ride);
            _unitOfWork.SaveChanges();
        }

        public async Task<IEnumerable<Ride>> GetAllAsync()
        {
            return await _unitOfWork.RidesRepo.GetAll();
        }

        public async Task<Ride> GetAsync(int id)
        {
            return await _unitOfWork.RidesRepo.GetById(id);
        }

        public async Task RemoveAsync(int id)
        {
            await _unitOfWork.RidesRepo.Delete(id);
            _unitOfWork.SaveChanges();
        }

        public Task<IEnumerable<Ride>> GetAllAsync(int clientId)
        {
            throw new NotImplementedException();
        }
    }
}
