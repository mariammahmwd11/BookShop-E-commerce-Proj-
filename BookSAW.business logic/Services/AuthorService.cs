using BookSAW.BL.DTO;
using BookSAW.business_logic.IServices;
using BookSAW.DataAccess.Repositories.IRepositories;
using BookSAW.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookSAW.business_logic.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork unitOfWork;

        public AuthorService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void addAuthor(AuthorDTO author)
        {
            var _author = new Author
            {
                Bio = author.Bio,
                Name = author.Name,
                Id = author.Id,
                ImageURl = author.ImageURl
            };
            unitOfWork.Author.Add(_author);
            unitOfWork.Save();
        }

        public void EditAuthor(AuthorDTO author)
        {
            var existingAuthor = unitOfWork.Author.GetByID(author.Id);
            if (existingAuthor == null)
            {
                return;
            }
            existingAuthor.Bio = author.Bio;
            existingAuthor.Name = author.Name;
            existingAuthor.ImageURl = author.ImageURl;
            unitOfWork.Author.Update(existingAuthor);
            unitOfWork.Save();


        }

        public AuthorDTO GetAuthor(int id)
        {
            var author=unitOfWork.Author.GetByID(id);
            AuthorDTO authorDTO = new AuthorDTO
            {
                Id = author.Id,
                Bio = author.Bio,
                Name = author.Name,
                ImageURl = author.ImageURl
            };

            return authorDTO;
        }

        public IEnumerable<AuthorDTO> GetAuthorList()
        {
            var books= unitOfWork.Author.GetAll();
            return books.Select(book => new AuthorDTO
            {
                Bio = book.Bio,
                Name = book.Name,
                Id = book.Id,
                ImageURl = book.ImageURl

            });
        }

        public void removeAuthor(int id)
        {
           unitOfWork.Author.Delete(id);
            unitOfWork.Save();
        }

        IEnumerable<AuthorDTO> IAuthorService.GetRecent5Authors()
        {
           var authors= unitOfWork.Author.GetTop5Authors();
            return authors.Select(a => new AuthorDTO
            {
                Id = a.Id,
                Bio = a.Bio,
                Name = a.Name,
                ImageURl = a.ImageURl,
                BookCount = a.Books.Count()
            });
        }
    }
}
