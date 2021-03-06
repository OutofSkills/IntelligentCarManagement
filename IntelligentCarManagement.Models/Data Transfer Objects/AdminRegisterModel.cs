using Models.Generics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Data_Transfer_Objects
{
    public class AdminRegisterModel: IRegisterModel
    {
        public byte[] Avatar { get; set; }

        [Required]
        [MinLength(3)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [DataType(DataType.EmailAddress)]

        public string Email { get; set; }

        [Required(ErrorMessage = "UserName is required."), MinLength(3)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [MinLength(9)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(4)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [MinLength(4)]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "The password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
