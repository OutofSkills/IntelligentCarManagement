using ClientUI.Utils;
using Models;
using Models.Data_Transfer_Objects;
using Models.DTOs;

namespace ClientUI.Services.Notifications
{
    public interface INotificationsService
    {
        Task<Utils.RequestResponse> AddCategoryAsync(NotificationCategory category);
        Task<Utils.RequestResponse> EditCategoryAsync(int id, NotificationCategory category);
        Task<Utils.RequestResponse> DeleteCategoryAsync(int id);
        Task<IEnumerable<NotificationCategory>> GetCategoriesAsync();
        Task<Utils.RequestResponse> SendAsync(string category, NotificationDTO notification);
    }
}
