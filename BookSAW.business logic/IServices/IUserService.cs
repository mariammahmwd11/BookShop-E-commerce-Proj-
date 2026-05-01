using BookSAW.business_logic.DTO.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookSAW.business_logic.IServices
{
    public interface IUserService
    {
        Task<UsersListDTO> GetUsersAsync();

        Task<(bool Success, IEnumerable<string> Errors)> CreateUserAsync(CreateUserDTO model);

        Task PromoteToAdminAsync(string userId);

        Task DemoteFromAdminAsync(string userId);

        Task ToggleLockoutAsync(string userId);

        Task DeleteUserAsync(string id);

        Task<ManageUserRolesDTO> GetUserRolesAsync(string userId);

        Task AssignRoleAsync(string userId, string role);

        Task RemoveRoleAsync(string userId, string role);
    }
}
