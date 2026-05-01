using BookSAW.DataAccess.Data;
using BookSAW.DataAccess.Repositories.IRepository;
using BookSAW.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookSAW.DataAccess.Repositories.Repository
{
    public class AuthorRepository : BasicRepository<Author>, IAuthorRepository
    {
        private readonly AppDbContext appDbContext;

        public AuthorRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            {
                this.appDbContext = appDbContext;
            }
        }

        IEnumerable<Author> IAuthorRepository.GetTop5Authors()
        {
           var authors = appDbContext.Authors.OrderByDescending(a => a.Id).Take(5)
                .Include(b=>b.Books).ToList();
            return authors;
        }
    }
}
