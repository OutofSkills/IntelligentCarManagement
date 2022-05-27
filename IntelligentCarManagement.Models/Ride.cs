using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Ride
    {
        public int Id { get; set; }
        public string PickUpPlaceName { get; set; }
        public string PickUpPlaceAddress { get; set; }
        public string PickUpPlaceLat { get; set; }
        public string PickUpPlaceLong { get; set; }
        public string DestinationPlaceName { get; set; }
        public string DestinationPlaceAddress { get; set; }
        public string DestinationPlaceLat { get; set; }
        public string DestinationPlaceLong { get; set; }
        public double Distance { get; set; }
        public double AverageTime { get; set; }
        public DateTime PickUpTime { get; set; }
        public int DriverId { get; set; }
        public int ClientId { get; set; }
        public int RideStateId { get; set; }

        [ForeignKey(nameof(RideStateId))]
        public virtual RideState RideState{ get; set; }

        [ForeignKey(nameof(DriverId))]
        public virtual Driver Driver { get; set; }

        [ForeignKey(nameof(ClientId))]
        public virtual Client Client { get; set; }
    }
}
