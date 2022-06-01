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
        [Key]
        public int Id { get; set; }
        [Required]
        public string PickUpPlaceName { get; set; }
        public string PickUpPlaceAddress { get; set; }
        [Required]
        public string PickUpPlaceLat { get; set; }
        [Required]
        public string PickUpPlaceLong { get; set; }
        [Required]
        public string DestinationPlaceName { get; set; }
        public string DestinationPlaceAddress { get; set; }
        [Required]
        public string DestinationPlaceLat { get; set; }
        [Required]
        public string DestinationPlaceLong { get; set; }
        [Required]
        public double Distance { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
        public double AverageTime { get; set; }
        public DateTime PickUpTime { get; set; }
        public int DriverId { get; set; }
        public int ClientId { get; set; }
        public int RideStateId { get; set; }
        public int? RideReviewId { get; set; }

        [ForeignKey(nameof(RideStateId))]
        public virtual RideState RideState{ get; set; }

        [ForeignKey(nameof(DriverId))]
        public virtual Driver Driver { get; set; }

        [ForeignKey(nameof(ClientId))]
        public virtual Client Client { get; set; }

        [ForeignKey(nameof(RideReviewId))]
        public virtual Review Review { get; set; }
    }
}
