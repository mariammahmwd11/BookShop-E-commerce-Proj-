using BookSAW.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookSAW.DataAccess.Repositories.IRepository
{
    public interface ICartRepository:IBasicRepository<Cart>
    {

        Cart GetByUserId(string userId);
    }
}
