using Api.Services.Utils;
using Models;
using Models.Data_Transfer_Objects;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Api.Services
{
    public interface INotificationService
    {
        Task<IEnumerable<NotificationDTO>> GetAsync(int userId);
        void Save(NotificationDTO notification, int id);
        Task RemoveAsync(int id);
        Task<RequestResponse> SendAsync(int userId, NotificationDTO notification);
        Task UpdateFirebaseToken(int userId, string token);
        Task<NotificationCategoryDTO> GetNotificationCategoryAsync(NotificationCategories category);
        void CreateCategory(NotificationCategory category);
        Task<IEnumerable<NotificationCategory>> GetCategoriesAsync();
        Task RemoveCategoryAsync(int id);
        void UpdateCategoryAsync(NotificationCategory category, int id);
    }
}
