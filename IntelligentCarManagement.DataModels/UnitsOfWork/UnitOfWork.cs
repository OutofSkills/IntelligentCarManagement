using Models;
using IntelligentCarManagement.DataAccess.Repositories;
using IntelligentCarManagement.DataAccess.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Text;
using IntelligentCarManagement.Api.DataAccess.Repositories;
using Api.DataAccess.Repositories.Interfaces;
using Api.DataAccess.Repositories.Implementation;

namespace IntelligentCarManagement.DataAccess.UnitsOfWork
{
    public class UnitOfWork: IUnitOfWork
    {
        public ICarsRepo CarsRepo { get; private set; }
        public IDriversRepo DriversRepo { get; private set; }
        public IClientsRepo ClientsRepo { get; private set; }
        public IUsersRepo UsersRepo { get; private set; }
        public IRolesRepo RolesRepo { get; private set; }
        public IAccountStatusesRepo StatusesRepo { get; private set; }
        public IDriverStatusRepo DriverStatusesRepo { get; private set; }
        public INotificationsRepo NotificationsRepo { get; private set; }
        public IAddressRepo AdressRepo { get; private set; }
        public IRidesRepo RidesRepo { get; private set; }
        public IDriverApplicationsRepo ApplicationsRepo { get; private set; }


        /// <summary>
        /// The DbContext
        /// </summary>
        private CarMngContext _context;

        /// <summary>
        /// Initializes a new instance of the UnitOfWork class.
        /// </summary>
        /// <param name="context">The object context</param>
        public UnitOfWork(CarMngContext context)
        {
            _context = context;
            CarsRepo = new CarsRepo(_context);
            DriversRepo = new DriversRepo(_context);
            ClientsRepo = new ClientsRepo(_context);
            UsersRepo = new UsersRepo(_context);
            RolesRepo = new RolesRepo(_context);
            StatusesRepo = new AccountStatusesRepo(_context);
            DriverStatusesRepo = new DriverStatusRepo(_context);
            NotificationsRepo = new NotificationsRepo(_context);
            RidesRepo = new RidesRepo(_context);
            ApplicationsRepo = new DriverApplicationsRepo(_context);
        }

        /// <summary>
        /// Saves all pending changes
        /// </summary>
        /// <returns>The number of objects in an Added, Modified, or Deleted state</returns>
        public int SaveChanges()
        {
            // Save changes with the default options
            return _context.SaveChanges();
        }

        /// <summary>
        /// Disposes the current object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes all external resources.
        /// </summary>
        /// <param name="disposing">The dispose indicator.</param>
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }
    }
}
