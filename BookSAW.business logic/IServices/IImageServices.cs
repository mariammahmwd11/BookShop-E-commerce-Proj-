using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookSAW.business_logic.IServices
{
    public interface IImageServices
    {
        Task<string> SaveImage(IFormFile photo);
        void DeleteCategoryImage(string imageUrl);


    }
}
