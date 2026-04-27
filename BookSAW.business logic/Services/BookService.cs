using BookSAW.BL.DTO;
using BookSAW.business_logic.IServices;
using BookSAW.DataAccess.Repositories.IRepositories;
using BookSAW.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookSAW.business_logic.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork unitOfWork;

        public BookService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public void creat(BookDTO book)
        {
            
            var NewBook = new Book
            {
                Name = book.Name,
                AuthorId = book.AuthorId,
                CategoryId = book.CategoryId,
                ImageUrl = book.ImageUrl,
                Description = book.Description,
                Price = book.Price,
                Stock = book.Stock,
                IsFeatured= book.IsFeatured,
                DiscountPercent = book.DiscountPercent ?? 0

            };
            unitOfWork.Book.Add(NewBook);
            unitOfWork.Save();
            
        }

        public void Delete(int Id)
        {
          
            unitOfWork.Book.Delete(Id);
            unitOfWork.Save();
        }

        public IEnumerable<BookDTO> GetAll()
        {
            var books=unitOfWork.Book.GetAllWithAuthorAndCategory();
            return books.Select(b => new BookDTO
            {
                BookID= b.BookID,
                Name = b.Name,
                AuthorId = b.AuthorId,
                CategoryId = b.CategoryId,
                ImageUrl = b.ImageUrl,
                Description = b.Description,
                Price = b.Price,
                Stock = b.Stock,
                AuthorName = b.Author?.Name,
                CategoryName = b.Category?.Name,
                 IsFeatured = b.IsFeatured,
                DiscountPercent = b.DiscountPercent

            });
        }

        public BookDTO GetById(int Id)
        {
            var book = unitOfWork.Book.GetByIdWithAuthorAndCategory(Id);
            var bookDto = new BookDTO
            {
                BookID= book.BookID,
                Name = book.Name,
                AuthorId = book.AuthorId,
                CategoryId = book.CategoryId,
                ImageUrl = book.ImageUrl,
                Description = book.Description,
                Price = book.Price,
                Stock = book.Stock,
                AuthorName = book.Author?.Name,
                CategoryName = book.Category?.Name,
                IsFeatured = book.IsFeatured,
                DiscountPercent = book.DiscountPercent
            };
            return bookDto;
        }

        public void togglefeature(int Id)
        {
            var book=unitOfWork.Book.GetByID(Id);
            if (book == null)
            {
                return;
            }
            book.IsFeatured = !book.IsFeatured;
            unitOfWork.Book.Update(book);
            unitOfWork.Save();
        }

        public void ToggleOffer(int bookId, int discountPercent)
        {
            var book=unitOfWork.Book.GetByID(bookId);
            if (book == null)
            {
                return;
            }
            if (book.DiscountPercent>0)
            {
                book.DiscountPercent = 0;
            }
            else
            {
               
                book.DiscountPercent = discountPercent;
            }

            unitOfWork.Book.Update(book) ;
            unitOfWork.Save();
        }

        public void Update(BookDTO book)
        {
            var existing = unitOfWork.Book.GetByID(book.BookID);
            if (existing == null)
            {
                return;
            }
           existing.Name = book.Name;
            existing.Description = book.Description;
            existing.CategoryId= book.CategoryId;
            existing.AuthorId= book.AuthorId;
            existing.Price = book.Price;
            existing.Stock = book.Stock;
            existing.ImageUrl = book.ImageUrl;
            existing.IsFeatured = book.IsFeatured;
            existing.DiscountPercent = book.DiscountPercent ?? 0;
            unitOfWork.Book.Update(existing);
            unitOfWork.Save();


        }

        public void UpdateStock(int Id, int newStock)
        {
            var book= unitOfWork.Book.GetByID(Id);
            if(book == null)
            { return; }
            book.Stock = newStock;
            unitOfWork.Book.Update(book);
            unitOfWork.Save();
        }

        IEnumerable<BookDTO> IBookService.GetAllFeaturedBooks()
        {
           var books= unitOfWork.Book.GetAllFeaturedBooks();
            return books.Select(b => new BookDTO
            {
                BookID = b.BookID,
                Name = b.Name,
                AuthorId = b.AuthorId,
                CategoryId = b.CategoryId,
                ImageUrl = b.ImageUrl,
                Description = b.Description,
                Price = b.Price,
                Stock = b.Stock,
                AuthorName = b.Author?.Name,
                CategoryName = b.Category?.Name,
                IsFeatured = b.IsFeatured,
                DiscountPercent = b.DiscountPercent
            });

        }

        IEnumerable<BookDTO> IBookService.GetAllOfferd()
        {
            var books= unitOfWork.Book.GetAllOfferedBooks();
            return books.Select(b => new BookDTO
            {
                BookID = b.BookID,
                Name = b.Name,
                AuthorId = b.AuthorId,
                CategoryId = b.CategoryId,
                ImageUrl = b.ImageUrl,
                Description = b.Description,
                Price = b.Price,
                Stock = b.Stock,
                AuthorName = b.Author?.Name,
                CategoryName = b.Category?.Name,
                IsFeatured = b.IsFeatured,
                DiscountPercent = b.DiscountPercent
            });
        }
    }
}
