using Models;
using Models.Data_Transfer_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Api.Services
{
    public interface INotificationService
    {
        Task<IEnumerable<Notification>> GetNotificationsAsync(int userId);
        void SaveNotification(Notification notification);
        Task RemoveNotificationAsync(int id);
        Task<NotificationResponse> SendNotification(NotificationDTO notification);
    }
}
