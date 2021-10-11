using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Models
{
    public class Ride
    {
        public int Id { get; set; }
        [Required]
        public string PickUpLocation { get; set; }
        public string PickUpCoordinates { get; set; }
        [Required]
        public string Destination { get; set; }
        public string DestinationCoordinates { get; set; }
        public DateTime PickUpTime { get; set; }
        public int? DriverId { get; set; }
        public int? ClientId { get; set; }

        [ForeignKey(nameof(DriverId))]
        public virtual Driver Driver { get; set; }

        [ForeignKey(nameof(ClientId))]
        public virtual User User { get; set; }
    }
}
