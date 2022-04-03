using IntelligentCarManagement.DataAccess.Repositories.GenericRepository;
using Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntelligentCarManagement.DataAccess.Repositories
{
    public class UsersRepo: Repo<UserBase>, IUsersRepo
    {
        public UsersRepo(CarMngContext context) : base(context) { }
    }
}
