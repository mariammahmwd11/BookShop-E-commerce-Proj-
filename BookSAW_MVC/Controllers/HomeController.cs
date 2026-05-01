using BookSAW.business_logic.IServices;
using Estra7a.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace BookSAW_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICartService cartService;

        public HomeController(ICartService cartService)
        {
            this.cartService = cartService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
       
    }
}
