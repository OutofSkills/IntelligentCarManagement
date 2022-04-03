using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.CustomExceptions
{
    public class InvalidAuthenticationException: Exception
    {
        public InvalidAuthenticationException(string message): base(message)
        { }
    }
}
