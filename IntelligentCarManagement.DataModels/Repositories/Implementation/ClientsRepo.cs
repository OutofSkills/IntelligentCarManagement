using IntelligentCarManagement.DataAccess.Repositories.GenericRepository;
using Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntelligentCarManagement.DataAccess.Repositories
{
    public class ClientsRepo: Repo<Client>, IClientsRepo
    {
        public ClientsRepo(CarMngContext context) : base(context) { }

        public Client GetByEmail(String email)
        {
            return _context.Clients.Where(d => d.Email == email).FirstOrDefault();
        }
    }
}
