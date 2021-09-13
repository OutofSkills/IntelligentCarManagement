using IntelligentCarManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Client.Services
{
    public interface INotificationsService
    {
        Task<IEnumerable<Notification>> GetUserNotifications(int userId);
        Task<bool> RemoveNotification(int id);
    }
}
