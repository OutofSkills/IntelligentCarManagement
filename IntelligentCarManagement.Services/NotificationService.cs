using IntelligentCarManagement.DataAccess.UnitsOfWork;
using IntelligentCarManagement.Models;
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

        public bool AddNewNotification(Notification notification)
        {
            var success = true;

            try
            {
                unitOfWork.NotificationsRepo.Insert(notification);
                unitOfWork.SaveChanges();
            }catch(Exception e)
            {
                // Do something with the exception
                return !success;
            }

            return success;
        }

        public async Task<IEnumerable<Notification>> GetUserNotificationsAsync(int userId)
        {
            return await unitOfWork.NotificationsRepo.GetUserNotificationsAsync(userId);
        }

        public async Task<bool> RemoveNotificationAsync(int id)
        {
            var success = true;

            try
            {
                await unitOfWork.NotificationsRepo.Delete(id);
                unitOfWork.SaveChanges();
            }
            catch (Exception e)
            {
                // Do something with the exception
                return !success;
            }

            return success;
        }
    }
}
