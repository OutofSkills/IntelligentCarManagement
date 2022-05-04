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
        Task<IEnumerable<NotificationResponse>> GetAsync(int userId);
        void Save(NotificationDTO notification);
        Task RemoveAsync(int id);
        Task<NotificationResponse> SendAsync(int userId, NotificationDTO notification);
    }
}
