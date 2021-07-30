using IntelligentCarManagement.Models;
using IntelligentCarManagement.DataAccess.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace IntelligentCarManagement.DataAccess.Repositories
{
    class DriversRepo:Repo<Driver>, IDriversRepo
    {
        public DriversRepo(CarMngContext context) : base(context) { }

        public Driver GetCarDriver(int carID)
        {
            return _context.Drivers.Where(d => d.Car.Id == carID).FirstOrDefault();
        }
    }
}
