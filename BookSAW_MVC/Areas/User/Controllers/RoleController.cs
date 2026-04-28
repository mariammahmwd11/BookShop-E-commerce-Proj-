using BookSAW.DataAccess.Data;
using BookSAW.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookSAW_MVC.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly UserManager<AppUser> userManager;

        public RoleController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<IActionResult> MakeAdmin(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound();

            await userManager.AddToRoleAsync(user, "Admin");

            return Content($"User {user.UserName} became Admin");
        }
    }
}
