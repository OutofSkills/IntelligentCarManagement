using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Data_Transfer_Objects
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public double Rating { get; set; }
        public double DrivingAccuracy { get; set; }
    }
}
