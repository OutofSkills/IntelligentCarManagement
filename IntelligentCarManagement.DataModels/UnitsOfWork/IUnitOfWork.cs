using IntelligentCarManagement.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentCarManagement.DataAccess.UnitsOfWork
{
    public interface IUnitOfWork: IDisposable
    {
        /// <summary>
        /// Repositories
        /// </summary>
        ICarsRepo CarsRepo { get; }
        IDriversRepo DriversRepo { get; }
        IUsersRepo UsersRepo { get; }

        /// <summary>
        /// Saves all pending changes
        /// </summary>
        /// <returns>The number of objects in an Added, Modified, or Deleted state</returns>
        int SaveChanges();
    }
}
