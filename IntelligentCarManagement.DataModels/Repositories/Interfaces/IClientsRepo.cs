using IntelligentCarManagement.DataAccess.Repositories.GenericRepository;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentCarManagement.DataAccess.Repositories
{
    public interface IClientsRepo: IRepo<Client>
    {
        public Client GetByEmail(String email);
    }
}
