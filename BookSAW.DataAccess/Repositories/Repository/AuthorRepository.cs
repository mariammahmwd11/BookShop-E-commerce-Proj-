using BookSAW.DataAccess.Data;
using BookSAW.DataAccess.Repositories.IRepository;
using BookSAW.Models.Models;
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
    }
}
