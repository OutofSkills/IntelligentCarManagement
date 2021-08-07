using IntelligentCarManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Client.Services
{
    public interface IUsersService
    {
        public Task<IEnumerable<User>> GetUsersAsync();
        public bool NewUser(User user);
        public bool ChangeAccountStatus(User user, string status);
    }
}
