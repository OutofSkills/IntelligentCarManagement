using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.Interfaces
{
    public interface IChangePassword
    {
        public Task<ResetPasswordDTO> ChangePassword(ResetPasswordDTO model);
    }
}
