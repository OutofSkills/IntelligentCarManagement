using Models.Data_Transfer_Objects;
using Models.DTOs;

namespace ClientUI.Services
{
    public interface IAdminService
    {
        Task<UserBaseDTO> GetAsync(int id);
        Task<IEnumerable<UserBaseDTO>> GetAllAsync();
        Task<RequestResponse> Register(AdminRegisterModel model);
    }
}
