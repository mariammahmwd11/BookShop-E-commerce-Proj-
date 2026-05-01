using BookSAW.business_logic.DTO.User;
using BookSAW.business_logic.IServices;
using BookSAW.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookSAW.business_logic.Services
{
    public class UserService:IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<UsersListDTO> GetUsersAsync()
            {
                var users = _userManager.Users.OrderBy(u => u.UserName).ToList();
                var rows = new List<UserDTO>();

                foreach (var u in users)
                {
                    var roles = await _userManager.GetRolesAsync(u);

                    rows.Add(new UserDTO
                    {
                        Id = u.Id,
                        UserName = u.UserName ?? "—",
                        Email = u.Email ?? "—",
                        EmailConfirmed = u.EmailConfirmed,
                        PhoneNumber = u.PhoneNumber,
                        IsLockedOut = u.LockoutEnd.HasValue && u.LockoutEnd > DateTimeOffset.UtcNow,
                        LockoutEnd = u.LockoutEnd,
                        AccessFailedCount = u.AccessFailedCount,
                        Roles = roles.ToList()
                    });
                }

                return new UsersListDTO { Users = rows };
            }

           
            public async Task<(bool Success, IEnumerable<string> Errors)> CreateUserAsync(CreateUserDTO model)
            {
                // تأكد إن الـ USER role موجود
                if (!await _roleManager.RoleExistsAsync("USER"))
                    await _roleManager.CreateAsync(new IdentityRole("USER"));

                if (!await _roleManager.RoleExistsAsync("ADMIN"))
                    await _roleManager.CreateAsync(new IdentityRole("ADMIN"));

                var user = new AppUser
                {
                    UserName = model.UserName.Trim(),
                    Email = model.Email.Trim(),
                    PhoneNumber = model.PhoneNumber?.Trim()
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                    return (false, result.Errors.Select(e => e.Description));

                
                await _userManager.AddToRoleAsync(user, "USER");

            
                if (model.PromoteToAdmin)
                    await _userManager.AddToRoleAsync(user, "ADMIN");

                return (true, Enumerable.Empty<string>());
            }

           
            public async Task PromoteToAdminAsync(string userId)
            {
                var user = await _userManager.FindByIdAsync(userId)
                           ?? throw new Exception("User not found");

                if (!await _roleManager.RoleExistsAsync("ADMIN"))
                    await _roleManager.CreateAsync(new IdentityRole("ADMIN"));

                if (!await _userManager.IsInRoleAsync(user, "ADMIN"))
                    await _userManager.AddToRoleAsync(user, "ADMIN");
            }

           
            public async Task DemoteFromAdminAsync(string userId)
            {
                var user = await _userManager.FindByIdAsync(userId)
                           ?? throw new Exception("User not found");

                if (await _userManager.IsInRoleAsync(user, "ADMIN"))
                    await _userManager.RemoveFromRoleAsync(user, "ADMIN");
            }

           
            public async Task ToggleLockoutAsync(string userId)
            {
                var user = await _userManager.FindByIdAsync(userId)
                           ?? throw new Exception("User not found");

                bool isLockedOut = user.LockoutEnd.HasValue && user.LockoutEnd > DateTimeOffset.UtcNow;

                if (isLockedOut)
                {
                   
                    await _userManager.SetLockoutEndDateAsync(user, null);
                    await _userManager.ResetAccessFailedCountAsync(user);
                }
                else
                {
                    
                    await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow.AddYears(100));
                }
            }

            
            public async Task DeleteUserAsync(string id)
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user != null) await _userManager.DeleteAsync(user);
            }

            
            public async Task<ManageUserRolesDTO> GetUserRolesAsync(string userId)
            {
                var user = await _userManager.FindByIdAsync(userId)
                               ?? throw new Exception("User not found");
                var allRoles = _roleManager.Roles.Select(r => r.Name!).ToList();
                var userRoles = await _userManager.GetRolesAsync(user);

                return new ManageUserRolesDTO
                {
                    UserId = user.Id,
                    UserName = user.UserName ?? "—",
                    Email = user.Email ?? "—",
                    Roles = allRoles.Select(r => new RoleSelectionDTO
                    {
                        RoleName = r,
                        IsAssigned = userRoles.Contains(r)
                    }).ToList()
                };
            }

            public async Task AssignRoleAsync(string userId, string role)
            {
                var user = await _userManager.FindByIdAsync(userId)
                           ?? throw new Exception("User not found");
                if (!await _userManager.IsInRoleAsync(user, role))
                    await _userManager.AddToRoleAsync(user, role);
            }

            public async Task RemoveRoleAsync(string userId, string role)
            {
                var user = await _userManager.FindByIdAsync(userId)
                           ?? throw new Exception("User not found");
                if (await _userManager.IsInRoleAsync(user, role))
                    await _userManager.RemoveFromRoleAsync(user, role);
            }
        }
    }

