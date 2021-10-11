using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IntelligentCarManagement.Models
{
    public class User: IdentityUser<int>
    {
        [Key]
        public override int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        public string LastName { get; set; }

        [Required]
        public int Age { get; set; }
  
        public override string PhoneNumber { get; set; }

        public DateTime RegistrationDate { get; set; }

        public string Avatar { get; set; }

        [DataType(DataType.EmailAddress)]

        public override string Email { get; set; }

        public override string UserName { get; set; }

        public string Access_Token { get; set; }

        public int? AddressId { get; set; }
        public int StatusId { get; set; }


        [ForeignKey(nameof(AddressId))]
        public virtual UserAddress Address { get; set; }

        [ForeignKey(nameof(StatusId))]
        public virtual AccountStatus AccountStatus { get; set; }

        public virtual IList<UserRole> UserRoles { get; set; }

        public virtual Driver Driver { get; set; }

        public virtual IEnumerable<Notification> Notifications { get; set; }

        public User()
        {
            UserRoles = new List<UserRole>();
            Notifications = new HashSet<Notification>();
        }
    }
}
