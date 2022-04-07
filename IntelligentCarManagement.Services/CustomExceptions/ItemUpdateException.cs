using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.CustomExceptions
{
    internal class ItemUpdateException: Exception
    {
        public ItemUpdateException(string message) : base(message)
        { }
    }
}
