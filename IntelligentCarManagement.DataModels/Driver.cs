using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentCarManagement.DataModels
{
    public class Driver: User
    {
        public IFormFile Licence { get; set; }
        public float Rating { get; set; }
        public Car Car { get; set; }
    }
}
