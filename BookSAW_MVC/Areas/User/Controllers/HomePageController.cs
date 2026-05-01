using BookSAW.business_logic.IServices;
using BookSAW.business_logic.Services;
using BookSAW_MVC.Areas.User.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookSAW_MVC.Areas.User.Controllers
{
    [Area("User")]
    [AllowAnonymous]
    public class HomePageController : BaseController
    {
        private readonly IBookService bookService;
        private readonly ICategoryService categoryService;
        private readonly ICartService cartService;

        public HomePageController(IBookService bookService,ICategoryService categoryService,ICartService cartService):base(cartService) 
        {
            this.bookService = bookService;
            this.categoryService = categoryService;
            this.cartService = cartService;
        }
        public IActionResult Index()
        {
            var model = new HomeVM
            {
                FeaturedBooks = bookService.GetAllFeaturedBooks(),
                OfferedBooks = bookService.GetAllOfferd(),
                AllBooks = bookService.GetAll(),
                Categories=categoryService.GetCategories(),
                CartBookIds= GetCartBookIds()
            };
            
            return View("Index",model);
          }
        public IActionResult GetAllBooks()
        {
            var model = new HomeVM
            {
               
                AllBooks = bookService.GetAll(),
                Categories= categoryService.GetCategories(),
                CartBookIds = GetCartBookIds()

            };
            return View("AllBooks",model);
        }
        public IActionResult GetFeaturedBooks()
        {
            var featuredBooks = new HomeVM
            {
                FeaturedBooks = bookService.GetAllFeaturedBooks(),
                CartBookIds = GetCartBookIds()
            };
            return View("FeaturedBooks", featuredBooks);
        }
        public IActionResult GetOfferedBooks()
        {
            var offeredBooks = new HomeVM
            {
                OfferedBooks = bookService.GetAllOfferd(),
                CartBookIds = GetCartBookIds()

            };
            return View("OfferedBooks", offeredBooks);
        
        }
        private HashSet<int> GetCartBookIds()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return new HashSet<int>();

            var cart = cartService.GetCart(userId);

            if (cart?.Items == null || !cart.Items.Any())
                return new HashSet<int>();

            return cart.Items
                .Select(i => i.BookId)
                .ToHashSet();
        }

       
        public IActionResult GetCount()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var count = userId != null ? cartService.GetCartTotal(userId) : 0;
            return Json(new { count = count });
        }
    }
}
