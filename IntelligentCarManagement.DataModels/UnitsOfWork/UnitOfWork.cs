using IntelligentCarManagement.Models;
using IntelligentCarManagement.DataAccess.Repositories;
using IntelligentCarManagement.DataAccess.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentCarManagement.DataAccess.UnitsOfWork
{
    public class UnitOfWork: IUnitOfWork
    {
        public ICarsRepo CarsRepo { get; private set; }
        public IDriversRepo DriversRepo { get; private set; }
        public IUsersRepo UsersRepo { get; private set; }

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
            UsersRepo = new UsersRepo(_context);

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
