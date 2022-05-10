using Models.DTOs;

namespace ClientUI.Services
{
    public interface IAdminService
    {
        Task<UserBaseDTO> Get(int id);
    }
}
