using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IntelligentCarManagement.Models
{
    public class Role: IdentityRole<int>
    {
        [Key]
        public override int Id { get; set; }
        public override string Name { get; set; }
        public string Description { get; set; }

        public virtual IList<UserRole> Users { get; set; }

        public Role()
        {
            Users = new List<UserRole>();
        }
    }
}
