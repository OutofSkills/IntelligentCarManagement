using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IntelligentCarManagement.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string FuelType { get; set; }
        public bool IsAvailable { get; set; }
        [NotMapped]
        public string CurrentLocation { get; set; }
        public string Latitude { get; set; }
        public string Longitude{ get; set;}
        public int? DriverID { get; set; }

        [ForeignKey("DriverID")]
        public virtual Driver Driver { get; set; }
    }
}
