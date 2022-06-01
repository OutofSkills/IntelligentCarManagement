using Models.Data_Transfer_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class NotificationDTO
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;

        public NotificationCategoryDTO NotificationCategory { get; set; }
    }
}
