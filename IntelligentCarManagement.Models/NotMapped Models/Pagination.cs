using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentCarManagement.Models.NotMapped_Models
{
    public class Pagination
    {
        public int Page { get; set; } = 1;
        public int NumberOfRecords { get; set; } = 10;
    }
}
