using Api.DataAccess.Repositories.Interfaces;
using IntelligentCarManagement.Api.DataAccess.Repositories;
using IntelligentCarManagement.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentCarManagement.DataAccess.UnitsOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Repositories
        /// </summary>
        ICarsRepo CarsRepo { get; }
        IDriversRepo DriversRepo { get; }
        IClientsRepo ClientsRepo { get; }
        IUsersRepo UsersRepo { get; }
        IRolesRepo RolesRepo { get; }
        IAccountStatusesRepo StatusesRepo{ get;}
        IDriverStatusRepo DriverStatusesRepo { get; }
        INotificationsRepo NotificationsRepo { get; }
        IAddressRepo AdressRepo { get; }
        IRidesRepo RidesRepo { get; }
        IDriverApplicationsRepo ApplicationsRepo { get; }  
        IApplicationStatusesRepo ApplicationStatusesRepo { get; }

        /// <summary>
        /// Saves all pending changes
        /// </summary>
        /// <returns>The number of objects in an Added, Modified, or Deleted state</returns>
        int SaveChanges();
    }
}
