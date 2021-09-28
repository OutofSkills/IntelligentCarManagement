using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentCarManagement.Models
{
    public class Driver
    {
        public int Id { get; set; }

        public string LicencePhoto { get; set; }

        public int Accidents { get; set; } = 0;

        public int DeservedClients { get; set; } = 0;

        public int Experience { get; set; } = 0;

        public float Rating { get; set; } = 0.0f;

        public string About { get; set; }

        public bool IsAvailable { get; set; }

        public int DriverStatusId { get; set; }
        public int UserId { get; set; }

        public virtual Car Car { get; set; }
        public virtual User User { get; set; }
        public virtual DriverStatus Status { get; set; }
    }
}
