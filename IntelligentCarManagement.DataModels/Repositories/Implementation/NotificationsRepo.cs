using IntelligentCarManagement.DataAccess;
using IntelligentCarManagement.DataAccess.Repositories.GenericRepository;
using Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Api.DataAccess.Repositories
{
    public class NotificationsRepo: Repo<Notification>, INotificationsRepo
    {
        public NotificationsRepo(CarMngContext context) : base(context) { }

        public async Task<IEnumerable<Notification>> GetUserNotificationsAsync(int userId)
        {
            return await _context.Notifications.Where(n => n.UserId == userId).ToListAsync();
        }
    }
}
