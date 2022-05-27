using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class RideState
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual IEnumerable<Ride> Rides { get; set; }
    }
}