using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentCarManagement.DataModels
{
    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string FuelType { get; set; }
        public bool IsAvailable { get; set; }
        public Driver Driver { get; set; }
    }
}
