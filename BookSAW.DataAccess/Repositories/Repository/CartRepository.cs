using BookSAW.DataAccess.Data;
using BookSAW.DataAccess.Repositories.IRepository;
using BookSAW.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookSAW.DataAccess.Repositories.Repository
{
    public class CartRepository : BasicRepository<Cart>,ICartRepository
    {
        private readonly AppDbContext appDbContext;

        public CartRepository(AppDbContext appDbContext): base(appDbContext)  
        {
            {
                this.appDbContext = appDbContext;
            }
        }

        public Cart GetByUserId(string userId)
        {
           
            return appDbContext.Carts
                .Include(c => c.Items)
                .ThenInclude(i => i.Book)
                .ThenInclude(b => b.Author)
                .FirstOrDefault(c => c.UserId == userId);
        }
    }
    }
