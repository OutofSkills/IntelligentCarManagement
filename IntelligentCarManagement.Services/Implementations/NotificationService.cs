using Api.Services.CustomExceptions;
using Api.Services.Utils;
using AutoMapper;
using CorePush.Google;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using IntelligentCarManagement.DataAccess.UnitsOfWork;
using Microsoft.Extensions.Options;
using Models;
using Models.Data_Transfer_Objects;
using Models.DTOs;
using Models.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

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

        public void Save(NotificationDTO notificationDTO, int userId)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<NotificationDTO, Models.Notification>();
                cfg.CreateMap<NotificationCategoryDTO, Models.NotificationCategory>();
            });

            IMapper iMapper = config.CreateMapper();
            var notification = iMapper.Map<NotificationDTO, Models.Notification>(notificationDTO);

            notification.UserId = userId;
            notification.NotificationCategoryId = notification.NotificationCategory.Id;
            notification.NotificationCategory = null;

            unitOfWork.NotificationsRepo.Insert(notification);
            unitOfWork.SaveChanges();
        }

        public async Task<IEnumerable<NotificationDTO>> GetAsync(int userId)
        {
            var notifications = await unitOfWork.NotificationsRepo.GetAll();
            notifications = notifications.Where(notification => notification.UserId == userId);

            var result = new List<NotificationDTO>();

            // Map the notification model to the notification DTO
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Models.Notification, NotificationDTO>();
                cfg.CreateMap<Models.NotificationCategory, NotificationCategoryDTO>();
            });

            IMapper iMapper = config.CreateMapper();

            foreach (var notification in notifications)
            {
                result.Add(iMapper.Map<Models.Notification, NotificationDTO>(notification));
            }

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            await unitOfWork.NotificationsRepo.Delete(id);
            unitOfWork.SaveChanges();
        }

        public async Task<RequestResponse> SendAsync(int userId, NotificationDTO notificationDTO)
        {
            // Get the user's firebase token
            var firebaseToken = await GetUserFirebaseToken(userId);

            RequestResponse response = new();
            try
            {
                var message = CreateNotification(notificationDTO, firebaseToken);
                var result = await messaging.SendAsync(message);

                this.Save(notificationDTO, userId);

                return new RequestResponse() { Success = true, Message = result };
            }
            catch (Exception ex)
            {
                return new RequestResponse() { Success = false, Message = "Something went wrong" };
            }
        }

        private async Task<string> GetUserFirebaseToken(int userId)
        {
            var user = await unitOfWork.UsersRepo.GetById(userId);
            if (user is null)
                throw new NotFoundException("User not found");

            if (String.IsNullOrEmpty(user.NotificationsToken))
                throw new Exception("This user doesn't have a firebase token yet.");

            return user.NotificationsToken;
        }

        private Message CreateNotification(NotificationDTO notification, string token)
        {
            return new Message()
            {
                Token = token,
                Notification = new FirebaseAdmin.Messaging.Notification()
                {
                    Body = notification.Body,
                    Title = notification.Title
                },
                Android = new AndroidConfig() 
                { 
                    Priority = Priority.High,
                    Notification = new AndroidNotification() { Icon = notification.NotificationCategory.Icon}
                }
            };
        }

        public async Task UpdateFirebaseToken(int userId, string token)
        {
            var user = await unitOfWork.UsersRepo.GetById(userId);
            if (user is null)
                throw new NotFoundException("User not found");

            user.NotificationsToken = token;

            unitOfWork.UsersRepo.Update(user);
            unitOfWork.SaveChanges();
        }

        public async Task<NotificationCategoryDTO> GetNotificationCategoryAsync(NotificationCategories category)
        {
            var notificationCategories = await unitOfWork.NotificationCategoriesRepo.GetAll();
            var notificationCategory = notificationCategories.Where(nc => nc.Name == category.ToString()).FirstOrDefault();

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<NotificationCategory, NotificationCategoryDTO>();
            });

            IMapper iMapper = config.CreateMapper();

            return iMapper.Map<NotificationCategory, NotificationCategoryDTO>(notificationCategory);
        }

        public void CreateCategory(NotificationCategoryDTO categoryDTO)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<NotificationCategoryDTO, NotificationCategory>();
                cfg.AddGlobalIgnore("Id");
            });

            IMapper iMapper = config.CreateMapper();

            var notificationCategory = new NotificationCategory();
            notificationCategory = iMapper.Map<NotificationCategoryDTO, NotificationCategory>(categoryDTO);

            unitOfWork.NotificationCategoriesRepo.Insert(notificationCategory);
            unitOfWork.SaveChanges();
        }
    }
}
