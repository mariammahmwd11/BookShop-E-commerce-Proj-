using BookSAW.business_logic.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace BookSAW_MVC.Areas.User.Controllers
{
    [Area("User")]
    public class BaseController : Controller
    {
        protected readonly ICartService cartService;

        public BaseController(ICartService cartService)
        {
            this.cartService = cartService;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            ViewBag.CartCount = 0;

            if (userId != null)
            {
                var cart = cartService.GetCart(userId);
                ViewBag.CartCount = cart?.Items?.Sum(i => i.Quantity) ?? 0;
            }

            base.OnActionExecuting(context);
        }
    }
    }

