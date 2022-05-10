using Models.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.Interfaces
{
    public interface ISignUp<T> where T : IRegisterModel
    {
        public Task Register(T model);
    }
}
