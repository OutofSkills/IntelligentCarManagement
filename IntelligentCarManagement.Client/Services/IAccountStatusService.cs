using IntelligentCarManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Client.Services
{
    public interface IAccountStatusService
    {
        Task<HttpResponseMessage> GetStatusesAsync(int page = 1, int recordsPerPage = 10);
        public Task<IEnumerable<AccountStatus>> GetStatusesAsync();
        Task<bool> RemoveStatusAsync(int id);
        Task<bool> EditStatusAsync(AccountStatus status);
        Task<AccountStatus> GetStatus(int id);
        Task<bool> AddStatus(AccountStatus status);
    }
}
