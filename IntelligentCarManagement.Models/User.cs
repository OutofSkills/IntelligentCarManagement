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
        [Required]
        [MinLength(8)]
        public override string PhoneNumber { get; set; }
        public DateTime RegistrationDate { get; set; }
        public byte[] Avatar { get; set; }
        [Required(ErrorMessage = "Email address is required.")]
        [DataType(DataType.EmailAddress)]
        public override string Email { get; set; }
        [Required(ErrorMessage = "Username is required.")]
        [MinLength(3)]
        public override string UserName { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [MinLength(4)]
        [DataType(DataType.Password)]
        [NotMapped]
        public string Password { get; set; }
        [Required]
        [MinLength(4)]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "The password do not match.")]
        [NotMapped]
        public string ConfirmPassword { get; set; }
        public string Access_Token { get; set; }
        public int AddressId { get; set; }
        public int StatusId { get; set; }
        public int RoleId { get; set; }


        [ForeignKey(nameof(AddressId))]
        public virtual UserAddress Address { get; set; }
        [ForeignKey(nameof(StatusId))]
        public virtual AccountStatus AccountStatus { get; set; }
        [ForeignKey(nameof(RoleId))]
        public virtual IList<UserRole> UserRoles { get; set; }

        public User()
        {
            UserRoles = new List<UserRole>();
            Address = new UserAddress();
        }
    }
}
