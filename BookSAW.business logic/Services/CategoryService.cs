using BookSAW.business_logic.IServices;
using BookSAW.DataAccess.Repositories.IRepositories;
using BookSAW.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookSAW.business_logic.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddCategory(CategoryDTO categoryDTO)
        {
            var category = new Category {
            Name=categoryDTO.Name,
            Id=categoryDTO.CategoryId,
            Description=categoryDTO.Description,
                ImageUrl=categoryDTO.ImageUrl


            };
            _unitOfWork.Category.Add(category);
            _unitOfWork.Save();
        }

        public bool DeleteCategory(int id)
        {
           var category = _unitOfWork.Category.GetByID(id);
            if (category == null)
            {
                return false;
            }
            _unitOfWork.Category.Delete(id);
            _unitOfWork.Save();
            return true;
        }

        public List<CategoryDTO> GetCategories()
        {
           var categories = _unitOfWork.Category.GetAll();
            return categories.Select(c => new CategoryDTO
            {
                CategoryId = c.Id,
                Name = c.Name,
                Description = c.Description,
                ImageUrl = c.ImageUrl
            }).ToList();

        }

        public CategoryDTO GetCategoryById(int id)
        {
           var category = _unitOfWork.Category.GetByID(id);
            if (category == null)
            {
                return null;
            }
            return new CategoryDTO
            {
                CategoryId = category.Id,
                Name = category.Name,
                Description = category.Description,
                ImageUrl = category.ImageUrl

            };
        }


        public void UpdateCategory(CategoryDTO categoryDTO)
        {var category = _unitOfWork.Category.GetByID(categoryDTO.CategoryId);
            if (category != null)
            {
                category.Name = categoryDTO.Name;
                category.Description = categoryDTO.Description;
                category.ImageUrl = categoryDTO.ImageUrl;

                _unitOfWork.Category.Update(category);
                _unitOfWork.Save();
            }
        }
    }
}
