using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class ClientDTO: UserBaseDTO
    {
        public double Rating { get; set; } = 0.0;
    }
}
