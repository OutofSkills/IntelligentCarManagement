using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Data_Transfer_Objects
{
    public class AddressDto
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Country { get; set; }
        public string County { get; set; }
        public string City { get; set; }

        public string ZipCode { get; set; }
    }
}
