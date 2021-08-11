using IntelligentCarManagement.DataAccess.Repositories.GenericRepository;
using IntelligentCarManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentCarManagement.DataAccess.Repositories
{
    public class AccountStatusesRepo: Repo<AccountStatus>, IAccountStatusesRepo
    {
        public AccountStatusesRepo(CarMngContext context) : base(context) { }
    }
}
