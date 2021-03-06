using IntelligentCarManagement.DataAccess.Repositories.GenericRepository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntelligentCarManagement.DataAccess.Repositories
{
    public class CarsRepo : Repo<Car>, ICarsRepo
    {
        public CarsRepo(CarMngContext context): base(context) {}
    }
}
