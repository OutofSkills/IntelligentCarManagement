using Api.Services.Utils;
using Models;
using Models.Generics;
using Models.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.Interfaces
{
    public interface IAccountService<T> where T : IRegisterModel
    {
        public Task<string> Login(LoginModel model);
        public Task Remove(int id);
        public Task Register(T model);
        public Task<ResetPasswordDTO> ChangePassword(ResetPasswordDTO model);
    }
}
