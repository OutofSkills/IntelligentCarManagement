using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
            //// Update parent object
            //table.Attach(obj);
            //_context.Entry(obj).State = EntityState.Modified;

            _context.Update(obj);
            //// Update children objects
            //IEnumerable<EntityEntry> unchangedEntities = _context.ChangeTracker.Entries().Where(x => x.State == EntityState.Unchanged);

            //foreach (EntityEntry ee in unchangedEntities)
            //{
            //    ee.State = EntityState.Modified;
            //}
        }
        public async Task Delete(object id)
        {
            TEntity existing = await table.FindAsync(id);
            table.Remove(existing);
        }
    }
}
