using BookSAW.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookSAW.business_logic.IServices
{
    public interface ICategoryService
    {
        CategoryDTO GetCategoryById(int id);
        List<CategoryDTO> GetCategories();
        void AddCategory(CategoryDTO categoryDTO);
        void UpdateCategory(CategoryDTO categoryDTO);
        bool DeleteCategory(int id);

   

    }
}
