using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class Driver: UserBase
    {
        public override int Id { get; set; }

        public string LicencePhoto { get; set; }

        public int Accidents { get; set; } = 0;

        public int DeservedClients { get; set; } = 0;

        public float Rating { get; set; } = 0.0f;

        public bool IsAvailable { get; set; }

        public int DriverStatusId { get; set; }
        public int CarId { get; set; }

        [ForeignKey(nameof(CarId))]
        public virtual Car Car { get; set; }
        [ForeignKey(nameof(DriverStatusId))]
        public virtual DriverStatus Status { get; set; }
    }
}
