using System;
using System.Collections.Generic;
using System.Text;

namespace BookSAW.DataAccess.Repositories.IRepositories
{
    public interface IUnitOfWork
    {
         ICategoryRepository Category { get; }
            void Save();
    }
}
