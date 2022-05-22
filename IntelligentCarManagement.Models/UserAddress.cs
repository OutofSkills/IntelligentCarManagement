using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class UserAddress
    {
        public int Id { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Country { get; set; }
        public string County { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
    }
}
