using Models;
using IntelligentCarManagement.DataAccess.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentCarManagement.DataAccess.Repositories
{
    public interface IDriversRepo: IRepo<Driver>
    {
        Driver GetCarDriver(int carID);
    }
}
