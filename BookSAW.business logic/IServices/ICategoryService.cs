using BookSAW.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookSAW.business_logic.IServices
{
    public interface ICategoryService
    {
        string GetCategoryById(int id);
        string GetCategoryByName(string categoryName);
        List<CategoryDTO> GetCategories();
        Category AddCategory(CategoryDTO categoryDTO);
        Category UpdateCategory(CategoryDTO categoryDTO);
        Category DeleteCategory(int id);

   

    }
}
