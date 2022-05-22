using Api.Services.CustomExceptions;
using AutoMapper;
using CorePush.Google;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using IntelligentCarManagement.DataAccess.UnitsOfWork;
using Microsoft.Extensions.Options;
using Models;
using Models.DTOs;
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

        public void Save(NotificationDTO notificationDTO)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<NotificationDTO, Models.Notification>();
            });

            IMapper iMapper = config.CreateMapper();
            var notification = iMapper.Map<NotificationDTO, Models.Notification>(notificationDTO);


            unitOfWork.NotificationsRepo.Insert(notification);
            unitOfWork.SaveChanges();
        }

        public async Task<IEnumerable<NotificationResponse>> GetAsync(int userId)
        {
            var notifications = await unitOfWork.NotificationsRepo.GetAll();

            var result = new List<NotificationDTO>();

            // Map the notification model to the notification DTO
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Models.Notification, NotificationDTO>();
            });

            IMapper iMapper = config.CreateMapper();

            foreach (var notification in notifications)
            {
                result.Add(iMapper.Map<Models.Notification, NotificationDTO>(notification));
            }

            return (IEnumerable<NotificationResponse>)result;
        }

        public async Task RemoveAsync(int id)
        {
            await unitOfWork.NotificationsRepo.Delete(id);
            unitOfWork.SaveChanges();
        }

        public async Task<NotificationResponse> SendAsync(int userId, NotificationDTO notificationDTO)
        {
            // Get the user's firebase token
            var firebaseToken = await GetUserFirebaseToken(userId);

            NotificationResponse response = new();
            try
            {
                var message = CreateNotification(notificationDTO.Title, notificationDTO.Body, firebaseToken);
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

        private async Task<string> GetUserFirebaseToken(int userId)
        {
            var user = await unitOfWork.UsersRepo.GetById(userId);
            if (user is null)
                throw new NotFoundException("User not found");

            if (String.IsNullOrEmpty(user.NotificationsToken))
                throw new Exception("This user doesn't have a firebase token yet.");

            return user.NotificationsToken;
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
                },
                Android = new AndroidConfig() 
                { 
                    Priority = Priority.High
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
    }
}
