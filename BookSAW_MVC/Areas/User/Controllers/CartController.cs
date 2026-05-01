using BookSAW.business_logic.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookSAW_MVC.Areas.User.Controllers
{
    [Area("User")]
    public class CartController : BaseController
    {
        private readonly ICartService cartService;

        public CartController(ICartService cartService) : base(cartService)
        {
            this.cartService = cartService;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var cart = cartService.GetCart(userId);
            return View(cart);
        }

        [HttpPost]
        public IActionResult AddToCart(int bookId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new { success = false, isAuthenticated = false, loginUrl = "/User/Account/Login?returnUrl=" + Request.Path });
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            cartService.AddToCart(userId, bookId);
            return Json(new { success = true, isAuthenticated = true });
        }

        [HttpPost]
        public IActionResult Increase(int itemId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            cartService.IncreaseQuantity(userId, itemId);
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult Decrease(int itemId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            cartService.DecreaseQuantity(userId, itemId);
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult Remove(int itemId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            cartService.RemoveItem(userId, itemId);
            return Json(new { success = true });
        }

        [HttpGet]
        public IActionResult GetCount()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var count = userId != null ? cartService.GetCartItemsCount(userId) : 0;
            return Json(new { count = count });
        }
    }
}