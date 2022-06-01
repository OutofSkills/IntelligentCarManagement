using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ClientReview
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public double Rating { get; set; }
        public int DriverId { get; set; }
        public int ClientId { get; set; }

        [ForeignKey(nameof(DriverId))]
        public virtual Driver Driver { get; set; }
        [ForeignKey(nameof(ClientId))]
        public virtual Client Client { get; set; }
    }
}
