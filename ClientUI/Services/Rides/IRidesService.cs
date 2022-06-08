using Models.Data_Transfer_Objects;

namespace ClientUI.Services.Rides
{
    public interface IRidesService
    {
        Task<IEnumerable<RideDTO>> GetAllAsync();
    }
}