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
    public class AddressRepo: Repo<UserAddress>, IAddressRepo
    {
        public AddressRepo(CarMngContext context) : base(context) { }
    }
}
