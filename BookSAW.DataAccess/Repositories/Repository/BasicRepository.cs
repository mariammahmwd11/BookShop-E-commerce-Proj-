using BookSAW.DataAccess.Data;
using BookSAW.DataAccess.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookSAW.DataAccess.Repositories.Repository
{
    public class BasicRepository<T> : IBasicRepository<T> where T : class
    {
        private readonly AppDbContext appContext;
        internal DbSet<T> dbSet;

        public BasicRepository(AppDbContext appContext)
        {
            this.appContext = appContext;
            this.dbSet = appContext.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(int id)
        {
            var entity = GetByID(id);
            if (entity != null)
            {
                dbSet.Remove(entity);
            }
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public T GetByID(int id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            var categries=dbSet.ToList();
            return categries;
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
        }
    }
}
