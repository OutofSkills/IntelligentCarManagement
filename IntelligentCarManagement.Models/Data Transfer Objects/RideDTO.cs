using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Data_Transfer_Objects
{
    public class RideDTO
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

        public RideStateDTO RideState { get; set; }
        public DriverDTO Driver { get; set; }
        public ClientDTO Client { get; set; }
    }
}
