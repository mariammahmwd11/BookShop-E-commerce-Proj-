using System;
using System.Collections.Generic;
using System.Text;

namespace BookSAW.business_logic.DTO.User
{
    public class RoleSelectionDTO
    {
        public string RoleName { get; set; } = string.Empty;
        public bool IsAssigned { get; set; }
    }
}
