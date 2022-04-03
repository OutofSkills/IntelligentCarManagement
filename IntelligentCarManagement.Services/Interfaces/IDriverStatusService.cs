using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Api.Services
{
    public interface IDriverStatusService
    {
        DriverStatus GetStatusByName(string name);
        Task<DriverStatus> GetStatusByIdAsync(string id);
    }
}
