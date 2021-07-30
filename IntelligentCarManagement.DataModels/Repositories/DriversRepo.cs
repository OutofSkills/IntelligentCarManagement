using IntelligentCarManagement.Models;
using IntelligentCarManagement.DataAccess.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentCarManagement.DataAccess.Repositories
{
    class DriversRepo:Repo<Driver>, IDriversRepo
    {
        public DriversRepo(CarMngContext context) : base(context) { }
    }
}
