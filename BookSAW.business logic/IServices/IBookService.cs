using BookSAW.BL.DTO;   
using System;
using System.Collections.Generic;
using System.Text;

namespace BookSAW.business_logic.IServices
{
    public interface IBookService
    {
        IEnumerable<BookDTO> GetAll();
        IEnumerable<BookDTO> GetAllFeaturedBooks();
        IEnumerable<BookDTO> GetAllOfferd();
        BookDTO GetById(int Id);
        void creat(BookDTO book);
        void Update(BookDTO book);
        void Delete(int Id);
        void togglefeature(int Id);
        void ToggleOffer(int bookId, int discountPercent);
        void UpdateStock(int Id,int newStock);


    }
}
