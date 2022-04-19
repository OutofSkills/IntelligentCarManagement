using Models;
using Models.Data_Transfer_Objects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Services
{
    public interface IClientsService
    {
        Task<ClientDTO> GetAsync(int id);
        ClientDTO Get(String email);
        ClientDTO Add(ClientDTO driver);
        Task<ClientDTO> UpdateAsync(int id, ClientDTO dto);
        Task<IEnumerable<ClientDTO>> GetAllAsync();
    }
}
