using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Others
{
    public class GoogleNotification
    {
        public class DataPayload
        {
            public string Title { get; set; }
            public string Body { get; set; }
        }
        public string Priority { get; set; } = "high";
        public DataPayload Data { get; set; }
        public DataPayload Notification { get; set; }
    }
}
