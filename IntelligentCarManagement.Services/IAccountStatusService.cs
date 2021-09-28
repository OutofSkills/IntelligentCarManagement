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
        Task RemoveStatusAsync(int id);
        void EditStatus(AccountStatus status);
        void AddStatus(AccountStatus status);
    }
}
