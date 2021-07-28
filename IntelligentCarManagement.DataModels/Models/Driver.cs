using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentCarManagement.DataAccess.Models
{
    public class Driver: User
    {
        public byte[] License { get; set; }
        public int DeservedClients { get; set; }
        public int Experience { get; set; }
        public float Rating { get; set; }
        public virtual Car Car { get; set; }
    }
}
