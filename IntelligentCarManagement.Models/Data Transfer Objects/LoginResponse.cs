using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class LoginResponse
    {
        public string JwtToken { get; set; }
        public string FirebaseToken { get; set; }
    }
}
