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
        public string EventName { get; set; }
        public string EventContent { get; set; }
        public DateTime DateTime { get; set; }
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual UserBase User { get; set; }
    }
}
