using BookSAW.DataAccess.Data;
using BookSAW.DataAccess.Repositories.IRepositories;
using BookSAW.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookSAW.DataAccess.Repositories.Repository
{
    public class CategoryRepository : BasicRepository<Category>, ICategoryRepository
    {
        private readonly AppDbContext appContext;

        public CategoryRepository(AppDbContext appContext) : base(appContext)
        {
            this.appContext = appContext;
        }
    }
}
