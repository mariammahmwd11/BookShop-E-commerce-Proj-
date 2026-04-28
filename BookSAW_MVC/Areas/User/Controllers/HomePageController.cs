using BookSAW.business_logic.IServices;
using BookSAW.business_logic.Services;
using BookSAW_MVC.Areas.User.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookSAW_MVC.Areas.User.Controllers
{
    [Area("User")]
    [AllowAnonymous]
    public class HomePageController : Controller
    {
        private readonly IBookService bookService;
        private readonly ICategoryService categoryService;

        public HomePageController(IBookService bookService,ICategoryService categoryService)
        {
            this.bookService = bookService;
            this.categoryService = categoryService;
        }
        public IActionResult Index()
        {
            var model = new HomeVM
            {
                FeaturedBooks = bookService.GetAllFeaturedBooks(),
                OfferedBooks = bookService.GetAllOfferd(),
                AllBooks = bookService.GetAll(),
                Categories=categoryService.GetCategories()
            };
            return View("Index",model);
          }
        public IActionResult GetAllBooks()
        {
            var model = new HomeVM
            {
               
                AllBooks = bookService.GetAll(),
                Categories= categoryService.GetCategories()
            };
            return View("AllBooks",model);
        }
        public IActionResult GetFeaturedBooks()
        {
            var featuredBooks = new HomeVM
            {
                FeaturedBooks = bookService.GetAllFeaturedBooks()
            };
            return View("FeaturedBooks", featuredBooks);
        }
        public IActionResult GetOfferedBooks()
        {
            var offeredBooks = new HomeVM
            {
                OfferedBooks = bookService.GetAllOfferd()
            };
            return View("OfferedBooks", offeredBooks);
        
        }
        
    }
}
