using IntelligentCarManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Client.Services
{
    public interface IUsersService
    {
        public Task<HttpResponseMessage> GetUsersAsync(int page = 1, int recordsPerPage = 10);
        public bool NewUser(User user);
        public bool ChangeAccountStatus(User user, string status);
    }
}
