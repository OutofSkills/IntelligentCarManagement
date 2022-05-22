using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Data_Transfer_Objects
{
    public class EmailDTO
    {
        public string SendTo { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Action { get; set; }
    }
}
