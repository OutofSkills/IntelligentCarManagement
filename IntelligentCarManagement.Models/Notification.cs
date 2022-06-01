using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime DateTime { get; set; }
        public int UserId { get; set; }
        public int NotificationCategoryId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual UserBase User { get; set; }

        [ForeignKey(nameof(NotificationCategoryId))]
        public virtual NotificationCategory NotificationCategory { get; set; }
    }
}
