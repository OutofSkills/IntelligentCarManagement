using IntelligentCarManagement.DataAccess.Repositories.GenericRepository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentCarManagement.DataAccess.Repositories
{
    public class RolesRepo : Repo<Role>, IRolesRepo
    {
        public RolesRepo(CarMngContext context) : base(context) { }
    }
}
