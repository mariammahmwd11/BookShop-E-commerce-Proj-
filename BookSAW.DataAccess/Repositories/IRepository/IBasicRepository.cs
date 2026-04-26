using System;
using System.Collections.Generic;
using System.Text;

namespace BookSAW.DataAccess.Repositories.IRepository
{
    public interface IBasicRepository<T> where T : class
    {
        T GetByID(int id);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
        void DeleteRange(IEnumerable<T> entities);
    }
}
