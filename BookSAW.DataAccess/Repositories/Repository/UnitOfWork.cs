using BookSAW.DataAccess.Data;
using BookSAW.DataAccess.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookSAW.DataAccess.Repositories.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext appDbContext;

        public ICategoryRepository Category {  get; private set; }
        public UnitOfWork( AppDbContext appDbContext) {
            this.appDbContext = appDbContext;
            Category = new CategoryRepository(appDbContext);
        }

        public void Save()
        {
            appDbContext.SaveChanges();
        }
    }
}
