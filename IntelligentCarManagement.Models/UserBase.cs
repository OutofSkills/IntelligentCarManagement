using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class UserBase: IdentityUser<int>
    {
        [Key]
        public override int Id { get; set; }

        [Required]
        [MinLength(3)]
        [RegularExpression("/^[a-z ,.'-]+$/i")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        [RegularExpression("/^[a-z ,.'-]+$/i")]
        public string LastName { get; set; }

        [Required]
        public int Age { get; set; }
  
        [DataType(DataType.PhoneNumber)]
        public override string PhoneNumber { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime RegistrationDate { get; set; }

        public byte[] Avatar { get; set; }

        [DataType(DataType.EmailAddress)]
        public override string Email { get; set; }

        [RegularExpression("^(?=.{8,20}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$")]
        public override string UserName { get; set; }

        public int? AddressId { get; set; }

        public int StatusId { get; set; }


        [ForeignKey(nameof(AddressId))]
        public virtual UserAddress Address { get; set; }

        [ForeignKey(nameof(StatusId))]
        public virtual AccountStatus AccountStatus { get; set; }

        public virtual IList<UserRole> UserRoles { get; set; }

        public virtual IEnumerable<Notification> Notifications { get; set; }

        public UserBase()
        {
            UserRoles = new List<UserRole>();
            Notifications = new HashSet<Notification>();
        }
    }
}
