using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class AccountStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual IEnumerable<UserBase> Users { get; set; }
    }
}
