using Models;
using Models.DTOs;
using Models.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.Interfaces
{
    public interface IDriversAccountService: ISignIn, IRemovable, IChangePassword
    {
        Task<string> CreateDriver(DriverApplication application);
    }
}
