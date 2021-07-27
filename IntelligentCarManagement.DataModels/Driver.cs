using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentCarManagement.DataModels
{
    public class Driver: User
    {
        public IFormFile License { get; set; }
        public int DeservedClients { get; set; }
        public int Experience { get; set; }
        public float Rating { get; set; }
        public Car Car { get; set; }
    }
}
