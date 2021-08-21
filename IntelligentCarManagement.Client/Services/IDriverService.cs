using IntelligentCarManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Client.Services
{
    public interface IDriverService
    {
        public Task<bool> AddDriver(Driver driver);
        public Task<bool> UpdateDriver(Driver driver);
    }
}
