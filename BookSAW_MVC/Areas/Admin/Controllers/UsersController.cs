using BookSAW.business_logic.DTO.User;
using BookSAW.business_logic.IServices;
using BookSAW.Models.Identity;
using BookSAW_MVC.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BookSAW_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class UsersController : Controller
    {
       
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
         
            this.userService = userService;
        }
    
 
            public async Task<IActionResult> Index()
            {
                var vm = await userService.GetUsersAsync();
                return View("Index",vm);
            }

            
            public IActionResult CreateUser()
            {
                return View(new CreateUserDTO());
            }

           
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> CreateUser(CreateUserDTO model)
            {
                if (!ModelState.IsValid)
                    return View(model);

                var (success, errors) = await userService.CreateUserAsync(model);

                if (!success)
                {
                    foreach (var e in errors)
                        ModelState.AddModelError(string.Empty, e);
                    return View(model);
                }

                TempData["SuccessMessage"] = model.PromoteToAdmin
                    ? $"Admin '{model.UserName}' created successfully."
                    : $"User '{model.UserName}' created successfully.";

                return RedirectToAction(nameof(Index));
            }

           
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> PromoteToAdmin(string userId, string userName)
            {
                try
                {
                    await userService.PromoteToAdminAsync(userId);
                    TempData["SuccessMessage"] = $"'{userName}' promoted to Admin.";
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                }
                return RedirectToAction(nameof(Index));
            }

            
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DemoteFromAdmin(string userId, string userName)
            {
                try
                {
                    await userService.DemoteFromAdminAsync(userId);
                    TempData["SuccessMessage"] = $"Admin role removed from '{userName}'.";
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                }
                return RedirectToAction(nameof(Index));
            }

           
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> ToggleLockout(string userId, string userName)
            {
                try
                {
                    await userService.ToggleLockoutAsync(userId);
                    TempData["SuccessMessage"] = $"Lockout status updated for '{userName}'.";
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                }
                return RedirectToAction(nameof(Index));
            }

           
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteUser(string id)
            {
                try
                {
                    await userService.DeleteUserAsync(id);
                    TempData["SuccessMessage"] = "User deleted successfully.";
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                }
                return RedirectToAction(nameof(Index));
            }

            public async Task<IActionResult> ManageRoles(string id)
            {
                if (string.IsNullOrWhiteSpace(id)) return NotFound();
                try
                {
                    var vm = await userService.GetUserRolesAsync(id);
                    return View(vm);
                }
                catch { return NotFound(); }
            }

            
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> AssignRole(string userId, string role)
            {
                try
                {
                    await userService.AssignRoleAsync(userId, role);
                    TempData["SuccessMessage"] = $"Role '{role}' assigned.";
                }
                catch (Exception ex) { TempData["ErrorMessage"] = ex.Message; }
                return RedirectToAction(nameof(ManageRoles), new { id = userId });
            }

            
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> RemoveRole(string userId, string role)
            {
                try
                {
                    await userService.RemoveRoleAsync(userId, role);
                    TempData["SuccessMessage"] = $"Role '{role}' removed.";
                }
                catch (Exception ex) { TempData["ErrorMessage"] = ex.Message; }
                return RedirectToAction(nameof(ManageRoles), new { id = userId });
            }
        }
    }

