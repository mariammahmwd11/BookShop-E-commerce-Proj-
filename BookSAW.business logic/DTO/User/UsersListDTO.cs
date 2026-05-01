using System;
using System.Collections.Generic;
using System.Text;

namespace BookSAW.business_logic.DTO.User
{
    public class UsersListDTO
    {
        public List<UserDTO> Users { get; set; } = new();
    }

}
