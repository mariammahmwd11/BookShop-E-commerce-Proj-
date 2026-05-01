using System;
using System.Collections.Generic;
using System.Text;

namespace BookSAW.business_logic.DTO.User
{
    public class ManageUserRolesDTO
    {
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<RoleSelectionDTO> Roles { get; set; } = new();
    }
}
