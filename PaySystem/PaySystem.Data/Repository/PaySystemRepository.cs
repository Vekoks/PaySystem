using PaySystem.Data.Content;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySystem.Data.Repository
{
    public class PaySystemRepository<T> : IPaySystemRepository<T> where T : class
    {
        private readonly IPaySystemDbContext _dbContext;

        public PaySystemRepository(IPaySystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }

        public IEnumerable<T> All()
        {
            return _dbContext.Set<T>().AsEnumerable();
        }

        public void Delete(object id)
        {
            var entity = this.GetById(id);

            if (entity != null)
            {
                this.Delete(entity);
            }
        }

        public void Delete(T entity)
        {
            var entry = this._dbContext.Entry(entity);
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                this._dbContext.Set<T>().Attach(entity);
                this._dbContext.Set<T>().Remove(entity);
            }
        }

        public T GetById(object id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public int SaveChanges()
        {
            return this._dbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            var entry = this._dbContext.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this._dbContext.Set<T>().Attach(entity);
            }

            entry.State = EntityState.Modified;
        }
    }
}
