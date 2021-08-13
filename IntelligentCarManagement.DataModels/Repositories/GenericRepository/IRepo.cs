using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentCarManagement.DataAccess.Repositories.GenericRepository
{
    public interface IRepo<T> where T: class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(object id);
        void Insert(T obj);
        void Update(T obj);
        Task Delete(object id);
    }
}
