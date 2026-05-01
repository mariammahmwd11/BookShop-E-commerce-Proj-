using BookSAW.DataAccess.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookSAW.DataAccess.Repositories.IRepositories
{
    public interface IUnitOfWork
    {
         ICategoryRepository Category { get; }
         IBookRepository Book { get; }
        IAuthorRepository Author { get; }
        ICartRepository Cart { get; }
            void Save();
    }
}
