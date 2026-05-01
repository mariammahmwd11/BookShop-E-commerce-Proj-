using BookSAW.business_logic.IServices;
using BookSAW.Models.Identity;
using BookSAW_MVC.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookSAW_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class DashboardAdminController : Controller
    {
        private readonly IBookService bookService;
        private readonly IAuthorService authorService;
        private readonly ICategoryService categoryService;
        private readonly UserManager<AppUser> userManager;

        public DashboardAdminController(IBookService bookService,IAuthorService authorService,ICategoryService categoryService,UserManager<AppUser> userManager)
        {
            this.bookService = bookService;
            this.authorService = authorService;
            this.categoryService = categoryService;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            var AllBooks = bookService.GetAll().Count();
            var BooksWithDiscount = bookService.GetAllOfferd().Count();
            var FeaturedBooks = bookService.GetAllFeaturedBooks().Count();
            var totalcategories = categoryService.GetCategories().Count();
            var totalAuthors = authorService.GetAuthorList().Count();
            var totalUsers = userManager.Users.Count();

            var vari = new AdminDashboardVM();


            vari.BooksWithDiscount = BooksWithDiscount;
            vari.TotalBooks = AllBooks;
               vari.TotalAuthors = totalAuthors;
            vari.TotalUsers = totalUsers;
            vari.TotalFeaturedBooks = FeaturedBooks;
            vari.TotalCategories = totalcategories;
            vari.RecentBooks = bookService.GetRecent5lBooks();
            vari.RecentCategories = categoryService.GetRecent5lCategories();
            vari.RecentAuthors = authorService.GetRecent5Authors();

            return View(vari);
            



            
      
        }
    }
}
