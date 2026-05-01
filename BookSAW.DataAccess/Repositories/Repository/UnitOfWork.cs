using BookSAW.DataAccess.Data;
using BookSAW.DataAccess.Repositories.IRepositories;
using BookSAW.DataAccess.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookSAW.DataAccess.Repositories.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext appDbContext;

        public ICategoryRepository Category {  get; private set; }
        public IBookRepository Book { get; private set; }

       public IAuthorRepository Author { get; private set; }
       public ICartRepository Cart { get; private set; }


        public UnitOfWork( AppDbContext appDbContext) {
            this.appDbContext = appDbContext;
            Category = new CategoryRepository(appDbContext);
            Book=new BookRepository(appDbContext);
            Author = new AuthorRepository(appDbContext);
            Cart = new CartRepository(appDbContext);
        }

        public void Save()
        {
            appDbContext.SaveChanges();
        }
    }
}
