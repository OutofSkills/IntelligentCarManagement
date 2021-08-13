using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentCarManagement.DataAccess.Repositories.GenericRepository
{
    public class Repo<TEntity> where TEntity: class
    {
        protected readonly CarMngContext _context = null;
        protected readonly DbSet<TEntity> table = null;

        public Repo(CarMngContext _context)
        {
            this._context = _context;
            table = _context.Set<TEntity>();
        }
        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await table.ToListAsync();
        }
        public async Task<TEntity> GetById(object id)
        {
            return await table.FindAsync(id);
        }
        public void Insert(TEntity obj)
        {
            table.Add(obj);
        }
        public void Update(TEntity obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
        public async Task Delete(object id)
        {
            TEntity existing = await table.FindAsync(id);
            table.Remove(existing);
        }
    }
}
