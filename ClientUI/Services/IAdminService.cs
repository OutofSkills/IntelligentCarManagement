using Models.DTOs;

namespace ClientUI.Services
{
    public interface IAdminService
    {
        Task<UserBaseDTO> GetAsync(int id);
        Task<IEnumerable<UserBaseDTO>> GetAllAsync();
    }
}
