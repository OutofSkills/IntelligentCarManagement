using IntelligentCarManagement.DataAccess.Repositories.GenericRepository;
using IntelligentCarManagement.Models;
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

        public IEnumerable<UserRole> GetUserRoles(int userId)
        {
            return (IEnumerable<UserRole>)_context.UserRoles.Where(ur => ur.UserId == userId);
        }
    }
}
