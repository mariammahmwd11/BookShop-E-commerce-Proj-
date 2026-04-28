using BookSAW.BL.DTO;
using BookSAW.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookSAW_MVC.Areas.User.Controllers
{
    [Area("User")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (ModelState.IsValid)
            {
                var existingEmail = await userManager.FindByEmailAsync(registerDTO.Email);
                var existingUser = await userManager.FindByEmailAsync(registerDTO.UserName);

                if (existingEmail != null)
                {
                    ModelState.AddModelError("", "Email already exists");
                    return View("Register", registerDTO);
                }
                else if(existingUser != null) {
                    ModelState.AddModelError("", "Username already exists");
                    return View("Register", registerDTO);
                }

                var newUser = new AppUser
                {
                    UserName = registerDTO.UserName,
                    Email = registerDTO.Email
                };

                var result = await userManager.CreateAsync(newUser, registerDTO.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(newUser, false);
                    return RedirectToAction("Login");
                }
            }

            return View("Register", registerDTO);
        }

        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(loginDTO.Email);

                if (user != null)
                {
                    var validpassword = await userManager.CheckPasswordAsync(user, loginDTO.Password);

                    if (validpassword)
                    {
                        await signInManager.SignInAsync(user, loginDTO.RememberMe);
                        return RedirectToAction("Index", "HomePage" );
                    }
                    else
                    {
                        ModelState.AddModelError("", "Wrong Email or PassWord");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "There is no User with this Email");
                }
            }

            return View("Login", loginDTO);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}