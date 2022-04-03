using IntelligentCarManagement.DataAccess.UnitsOfWork;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Api.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork unitOfWork;

        public NotificationService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void AddNewNotification(Notification notification)
        {
            unitOfWork.NotificationsRepo.Insert(notification);
            unitOfWork.SaveChanges();
        }

        public async Task<IEnumerable<Notification>> GetUserNotificationsAsync(int userId)
        {
            return await unitOfWork.NotificationsRepo.GetUserNotificationsAsync(userId);
        }

        public async Task RemoveNotificationAsync(int id)
        {
            await unitOfWork.NotificationsRepo.Delete(id);
            unitOfWork.SaveChanges();
        }
    }
}
