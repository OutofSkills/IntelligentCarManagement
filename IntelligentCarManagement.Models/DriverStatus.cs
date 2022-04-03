using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class DriverStatus
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual IEnumerable<Driver> Drivers { get; set; }

        public DriverStatus()
        {
            Drivers = new HashSet<Driver>();
        }
    }
}
