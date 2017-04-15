using System.Collections.Generic;

namespace PaySystem.Data.Repository
{
    public interface IPaySystemRepository<T> where T : class
    {
        IEnumerable<T> All();

        T GetById(object id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Delete(object id);

        int SaveChanges();
    }
}
