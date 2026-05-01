using BookSAW.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookSAW.DataAccess.Repositories.IRepository
{
    public interface IAuthorRepository:IBasicRepository<Author>
    {
        IEnumerable<Author> GetTop5Authors();
    }
}
