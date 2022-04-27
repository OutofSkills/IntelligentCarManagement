using CorePush.Google;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using IntelligentCarManagement.DataAccess.UnitsOfWork;
using Microsoft.Extensions.Options;
using Models;
using Models.Data_Transfer_Objects;
using Models.Others;
using Models.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static Models.Others.GoogleNotification;

namespace IntelligentCarManagement.Api.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly FirebaseMessaging messaging;

        public NotificationService(IUnitOfWork unitOfWork, IOptions<FcmNotificationSettings> settings)
        {
            this.unitOfWork = unitOfWork;
               
            messaging = FirebaseMessaging.GetMessaging(FirebaseApp.DefaultInstance);
        }

        public void SaveNotification(Models.Notification notification)
        {
            unitOfWork.NotificationsRepo.Insert(notification);
            unitOfWork.SaveChanges();
        }

        public async Task<IEnumerable<Models.Notification>> GetNotificationsAsync(int userId)
        {
            return await unitOfWork.NotificationsRepo.GetUserNotificationsAsync(userId);
        }

        public async Task RemoveNotificationAsync(int id)
        {
            await unitOfWork.NotificationsRepo.Delete(id);
            unitOfWork.SaveChanges();
        }

        public async Task<NotificationResponse> SendNotification(NotificationDTO notificationDTO)
        {
            NotificationResponse response = new NotificationResponse();
            try
            {
                var message = CreateNotification(notificationDTO.Title, notificationDTO.Body, notificationDTO.DeviceId);
                var data = new Dictionary<string, string>();
                var result = await messaging.SendAsync(message);
               
                response.IsSuccess = true;
                response.Message = result;
                
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Something went wrong";
                return response;
            }
        }

        private Message CreateNotification(string title, string notificationBody, string token)
        {
            return new Message()
            {
                Token = token,
                Notification = new FirebaseAdmin.Messaging.Notification()
                {
                    Body = notificationBody,
                    Title = title
                }
            };
        }
    }
}
