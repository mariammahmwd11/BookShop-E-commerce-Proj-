using BookSAW.BL.DTO;
using BookSAW.business_logic.IServices;
using Microsoft.AspNetCore.Mvc;

namespace BookSAW_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IImageServices imageServices;

        public CategoryController(ICategoryService categoryService, IImageServices imageServices)
        {
            _categoryService = categoryService;
            this.imageServices = imageServices;
        }

        public IActionResult Index()
        {
            var categories = _categoryService.GetCategories();
            return View(categories);
        }



        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryDTO dto)
        {
             if (!ModelState.IsValid)
            { 

                return View(dto);
            }   

                if (dto.Photo != null)
            {
                dto.ImageUrl = await imageServices.SaveImage(dto.Photo, "categories");
            }

            _categoryService.AddCategory(dto);

            TempData["Success"] = "Category added successfully";
            return RedirectToAction("Index");
        }



        public IActionResult Edit(int id)
        {


            var category = _categoryService.GetCategoryById(id);


            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryDTO dto)
        {
            if (!ModelState.IsValid)
            {
                
                return View(dto);
            }

            var existingCategory = _categoryService.GetCategoryById(dto.CategoryId);

            if (existingCategory == null)
                return NotFound();

           
            if (dto.Photo != null && dto.Photo.Length > 0)
            {
               
                imageServices.DeleteImage(existingCategory.ImageUrl);

                dto.ImageUrl =await imageServices.SaveImage(dto.Photo, "categories");
            }
            else
            {
             
                dto.ImageUrl = existingCategory.ImageUrl;
            }

            _categoryService.UpdateCategory(dto);

            TempData["Success"] = "Category updated successfully";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var category = _categoryService.GetCategoryById(id);

            if (category != null)
            {
                imageServices.DeleteImage(category.ImageUrl);
                _categoryService.DeleteCategory(id);
            }

            TempData["Success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }


     
        }
    }
    
