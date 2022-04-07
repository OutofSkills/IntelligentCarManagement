using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.CustomExceptions
{
    public class ServerException: Exception
    {
        public ServerException(String message): base(message) { }
    }
}
