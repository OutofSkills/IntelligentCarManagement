using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentCarManagement.DataModels
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public UserAddress Address { get; set; }
    }
}
