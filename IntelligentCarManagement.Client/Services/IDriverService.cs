using IntelligentCarManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Client.Services
{
    public interface IDriverService
    {
        public Task<bool> AddDriver(Driver driver);
        public Task<bool> UpdateDriver(Driver driver);
        public Task<HttpResponseMessage> GetDriversAsync(int page = 1, int recordsPerPage = 10);
        public Task<IEnumerable<Driver>> GetAllDriversAsync();
        Task<bool> DeclineDriverRequest(int id);
        Task<bool> AcceptDriverRequest(int id);
    }
}
