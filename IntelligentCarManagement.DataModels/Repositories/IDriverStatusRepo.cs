using IntelligentCarManagement.DataAccess.Repositories.GenericRepository;
using IntelligentCarManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Api.DataAccess.Repositories
{
    public interface IDriverStatusRepo : IRepo<DriverStatus>
    {
        DriverStatus GetByName(string name);
    }
}
