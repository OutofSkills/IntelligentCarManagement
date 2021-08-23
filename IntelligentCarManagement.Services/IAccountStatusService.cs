using IntelligentCarManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Api.Services
{
    public interface IAccountStatusService
    {
        Task<IEnumerable<AccountStatus>> GetAllStatusesAsync();
        Task<AccountStatus> GetStatusAsync(int id);
        Task<bool> RemoveStatusAsync(int id);
        bool EditStatusAsync(AccountStatus status);
        bool AddStatus(AccountStatus status);
    }
}
