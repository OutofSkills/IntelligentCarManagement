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
        public bool ChangeAccountStatus(User user, string status);
        public Task<bool> Edit(User user);
        public Task<HttpResponseMessage> GetUserAsync(int userId);
        public Task<bool> DeleteAccountAsync(int userId);
        Task<bool> UpdateUserRoles(User user);
        Task<IEnumerable<string>> GetUserRolesAsync(int userId);
    }
}
