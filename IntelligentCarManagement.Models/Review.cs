using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        public double? Rating { get; set; }
        public double? DrivingAccuracy { get; set; }

        public virtual Ride Ride { get; set; }
    }
}
