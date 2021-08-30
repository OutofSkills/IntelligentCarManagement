using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Models
{
    public class Ride
    {
        public int Id { get; set; }
        public string PickUpLocation { get; set; }
        public string Destination { get; set; }
        public DateTime SheduledTime { get; set; }
    }
}
