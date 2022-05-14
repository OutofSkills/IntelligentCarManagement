using IntelligentCarManagement.DataAccess;
using IntelligentCarManagement.DataAccess.Repositories.GenericRepository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Api.DataAccess.Repositories
{
    public class DriverStatusRepo: Repo<ApplicationStatus>, IDriverStatusRepo
    {
        public DriverStatusRepo(CarMngContext context) : base(context) { }

        public ApplicationStatus GetByName(string name)
        {
            return _context.ApplicationStatuses.Where(s => s.Name == name).FirstOrDefault();
        }
    }
}
