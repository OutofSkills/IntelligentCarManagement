using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IntelligentCarManagement.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
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
        public string PhoneNumber { get; set; }
        public DateTime RegistrationDate { get; set; }
        public byte[] Avatar { get; set; }
        [Required(ErrorMessage = "Email address is required.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [MinLength(3)]
        public string Username { get; set; }
        [Required]
        [MinLength(4)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Access_Token { get; set; }
        public int AddressId { get; set; }
        public int RoleId { get; set; }
        public int StatusId { get; set; }

        [ForeignKey("AddressId")]
        public virtual UserAddress Address { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
        [ForeignKey("StatusId")]
        public virtual AccountStatus AccountStatus { get; set; }
    }
}
