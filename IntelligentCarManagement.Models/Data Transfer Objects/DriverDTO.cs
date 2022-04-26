using Models.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Data_Transfer_Objects
{
    public class DriverDTO: UserBaseDTO
    {
        public int DeservedClients { get; set; } = 0;
        public float Rating { get; set; } = 0.0f;
        public bool IsAvailable { get; set; }
        public string CurrentLat { get; set; }
        public string CurrentLong { get; set; }
    }
}
