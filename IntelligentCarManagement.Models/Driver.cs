using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentCarManagement.Models
{
    public class Driver
    {
        public int Id { get; set; }
        public string LicensePhoto { get; set; }
        public int Accidents { get; set; } = 0;
        public int DeservedClients { get; set; } = 0;
        public int Experience { get; set; } = 0;
        public float Rating { get; set; } = 0.0f;
        public int UserId { get; set; }
        public virtual Car Car { get; set; }
        public virtual User User { get; set; }
    }
}
