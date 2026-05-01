using BookSAW.BL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookSAW.business_logic.IServices
{
    public interface IAuthorService
    {
        AuthorDTO GetAuthor(int id);
        IEnumerable<AuthorDTO> GetAuthorList();
        void addAuthor(AuthorDTO author);
        void removeAuthor(int id);
        void EditAuthor(AuthorDTO author);
        IEnumerable<AuthorDTO> GetRecent5Authors();

    }
}
