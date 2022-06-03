using ClientUI.Utils;
using Models.Data_Transfer_Objects;

namespace ClientUI.Services.Cars
{
    public interface ICarsService
    {
        Task<RequestResponse> CreateAsync(CarDTO dto);
        Task<IEnumerable<CarDTO>> GetAllAsync();
        Task<RequestResponse> AssignCarAsync(int carId, string driverEmail);
    }
}
