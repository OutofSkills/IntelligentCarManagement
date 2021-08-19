using IntelligentCarManagement.DataAccess.Repositories.GenericRepository;
using IntelligentCarManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentCarManagement.DataAccess.Repositories
{
    public interface IRolesRepo: IRepo<Role>
    {
        IEnumerable<UserRole> GetUserRoles(int userId);
    }
}
