using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.Interfaces
{
    public interface IDriverApplicationsService
    {
        void Apply(DriverApplicationDTO model);
        Task<IEnumerable<DriverApplicationDTO>> GetAll();
        Task<DriverApplicationDTO> GetAsync(int id);
        Task Approve(int id);
    }
}
