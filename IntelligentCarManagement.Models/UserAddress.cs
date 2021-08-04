using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentCarManagement.Models
{
    public class UserAddress
    {
        public int Id { get; set; }
        public string County { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public virtual IEnumerable<User> Users{ get; set; }

        public UserAddress()
        {
            Users = new HashSet<User>();
        }
    }
}
