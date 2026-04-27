using BookSAW.DataAccess.Data;
using BookSAW.DataAccess.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookSAW.DataAccess.Repositories.Repository
{
    public class BookRepository : BasicRepository<Book>, IBookRepository
    {
        private readonly AppDbContext appDbContext;

        public BookRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public IEnumerable<Book> GetAllWithAuthorAndCategory()
        {
            var books = appDbContext.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .ToList();
            return books;
        }
        public Book GetByIdWithAuthorAndCategory(int id)
        {
            var book = appDbContext.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .FirstOrDefault(b => b.BookID == id);
            return book;
        }

        IEnumerable<Book> IBookRepository.GetAllFeaturedBooks()
        {
           var books = appDbContext.Books
                .Where(b => b.IsFeatured==true)
                .Include(b => b.Author)
                .Include(b => b.Category)
                .ToList();
            return books;
        }

        IEnumerable<Book> IBookRepository.GetAllOfferedBooks()
        {
            var books = appDbContext.Books
                .Where(b => b.DiscountPercent > 0)
                .Include(b => b.Author)
                .Include(b => b.Category)
                .ToList(); 
            
            return books;
        }
    }
}
