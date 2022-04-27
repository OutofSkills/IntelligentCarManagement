using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Data_Transfer_Objects
{
    public class NotificationResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
