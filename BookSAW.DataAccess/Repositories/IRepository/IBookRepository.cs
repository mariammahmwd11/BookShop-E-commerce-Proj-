using System;
using System.Collections.Generic;
using System.Text;

namespace BookSAW.DataAccess.Repositories.IRepository
{
    public interface IBookRepository:IBasicRepository<Book>
    {
        IEnumerable<Book> GetAllWithAuthorAndCategory();
        Book GetByIdWithAuthorAndCategory(int id);
        IEnumerable<Book> GetAllFeaturedBooks();
        IEnumerable<Book> GetAllOfferedBooks();
        IEnumerable<Book> GetTop5Books();
    }
}
