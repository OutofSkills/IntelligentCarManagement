using ClientUI.Utils;
using Models.Data_Transfer_Objects;

namespace ClientUI.Services.Roles
{
    public interface IRolesService
    {
        Task<IEnumerable<RoleDTO>> GetAllAsync();
        Task<RequestResponse> Add(RoleDTO role);
        Task<RequestResponse> Delete(int id);
        Task<RequestResponse> Edit(int id, RoleDTO role);
    }
}
