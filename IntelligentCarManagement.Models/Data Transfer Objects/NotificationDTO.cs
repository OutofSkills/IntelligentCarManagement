using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Data_Transfer_Objects
{
    public class NotificationDTO
    {
        public string DeviceId { get; set; }
        public bool IsAndroiodDevice { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
