using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentCarManagement.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual IEnumerable<User> Users { get; set; }

        public Role()
        {
            Users = new HashSet<User>();
        }
    }
}
