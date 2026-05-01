using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookSAW.business_logic.DTO.User
{
            public class UserDTO
            {
                public string Id { get; set; } = string.Empty;
                public string UserName { get; set; } = string.Empty;
                public string Email { get; set; } = string.Empty;
                public bool EmailConfirmed { get; set; }
                public string? PhoneNumber { get; set; }
                public bool IsLockedOut { get; set; }
                public DateTimeOffset? LockoutEnd { get; set; }
                public int AccessFailedCount { get; set; }
                public List<string> Roles { get; set; } = new();
                public bool IsAdmin => Roles.Contains("ADMIN", StringComparer.OrdinalIgnoreCase);
            }

            
        }