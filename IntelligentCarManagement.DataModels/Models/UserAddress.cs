using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentCarManagement.DataAccess.Models
{
    public class UserAddress
    {
        public int Id { get; set; }
        public string County { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public virtual User User{ get; set; }
    }
}
