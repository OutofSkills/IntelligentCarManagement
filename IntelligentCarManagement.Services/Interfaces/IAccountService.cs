using Models;
using Models.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<string> Login(LoginModel model);
        public Task Remove(int id);
        public Task Register(RegisterModel model);
        public Task ChangePassword(ResetPasswordModel model);
    }
}
