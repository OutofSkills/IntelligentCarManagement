using IntelligentCarManagement.Models;
using IntelligentCarManagement.DataAccess.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentCarManagement.DataAccess.Repositories
{
    public interface ICarsRepo: IRepo<Car>
    {
        public IEnumerable<Car> GetAvailableCars();
    }
}
