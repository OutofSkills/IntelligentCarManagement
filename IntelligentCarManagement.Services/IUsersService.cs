using IntelligentCarManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Services
{
    public interface IUsersService
    {
        IEnumerable<User> GetAllUsers();
        User GetUser(int id);
        bool RemoveUser(User user);
        bool AddUser(User user);
        bool EditUser(User user);
        Task<string> RegisterUser(User user);
    }
}
