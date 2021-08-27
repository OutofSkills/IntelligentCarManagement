using IntelligentCarManagement.DataAccess;
using IntelligentCarManagement.DataAccess.Repositories.GenericRepository;
using IntelligentCarManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Api.DataAccess.Repositories
{
    public class DriverStatusRepo: Repo<DriverStatus>, IDriverStatusRepo
    {
        public DriverStatusRepo(CarMngContext context) : base(context) { }

        public DriverStatus GetByName(string name)
        {
            return _context.DriverStatuses.Where(s => s.Name == name).FirstOrDefault();
        }
    }
}
