using Models.DTOs;

namespace ClientUI.Services.Clients
{
    public interface IClientsService
    {
        Task<IEnumerable<ClientDTO>> GetAllAsync();
    }
}
