using BookSAW.BL.DTO;
using BookSAW.business_logic.IServices;
using Microsoft.AspNetCore.Mvc;

namespace BookSAW_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var categories = _categoryService.GetCategories();
            return View(categories);
        }

        // ================= CREATE =================

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            _categoryService.AddCategory(dto);

            TempData["Success"] = "Category added successfully";
            return RedirectToAction("Index");
        }

        // ================= EDIT =================

        public IActionResult Edit(int id)
        {
            var category = _categoryService.GetCategoryById(id);

            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CategoryDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            _categoryService.UpdateCategory(dto);

            TempData["Success"] = "Category updated successfully";
            return RedirectToAction("Index");
        }

        // ================= DELETE =================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _categoryService.DeleteCategory(id);

            TempData["Success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}