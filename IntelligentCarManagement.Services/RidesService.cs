using IntelligentCarManagement.DataAccess.UnitsOfWork;
using IntelligentCarManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Api.Services
{
    public class RidesService : IRidesService
    {
        private readonly IUnitOfWork unitOfWork;

        public RidesService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public int AddRide(Ride ride)
        {
            if (ride is null)
                throw new Exception("No data provided");

            if (ride.PickUpTime == default)
                ride.PickUpTime = DateTime.Now;

            unitOfWork.RidesRepo.Insert(ride);
            unitOfWork.SaveChanges();

            return ride.Id;
        }

        public async Task AssignDriverToRideAsync(int rideId, int driverId)
        {
            var ride = await unitOfWork.RidesRepo.GetById(rideId);
            if (ride is null)
                throw new Exception("Couldn't find information about the ride with the given id");

            var driver = await unitOfWork.DriversRepo.GetById(driverId);
            if (driver is null)
                throw new Exception("Couldn't find information about the driver with the given id");

            ride.DriverId = driverId;
            unitOfWork.RidesRepo.Update(ride);
            unitOfWork.SaveChanges();

        }

        public void EditRide(Ride ride)
        {
            unitOfWork.RidesRepo.Update(ride);
            unitOfWork.SaveChanges();
        }

        public async Task<IEnumerable<Ride>> GetAllRides()
        {
            return await unitOfWork.RidesRepo.GetAll();
        }

        public async Task<Ride> GetRideAsync(int id)
        {
            return await unitOfWork.RidesRepo.GetById(id);
        }

        public async Task RemoveRideAsync(int id)
        {
            await unitOfWork.RidesRepo.Delete(id);
            unitOfWork.SaveChanges();
        }
    }
}
