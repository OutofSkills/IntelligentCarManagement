using Models.Generics;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Data_Transfer_Objects;

namespace Models.DTOs
{
    public class DriverApplicationDTO
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

        public byte[] CV { get; set; }
        [Required]
        public bool OwnsACar { get; set; }
        public string ContactMethod { get; set; }

        public AddressDto Address { get; set; } = new AddressDto();
    }
}
