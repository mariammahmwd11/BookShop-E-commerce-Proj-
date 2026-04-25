using BookSAW.BL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookSAW.BL.IServices
{
    public interface IAccountService
    {
        Task<AuthResult> RegisterAsync(RegisterDTO registerDTO);
        Task<AuthResult> LoginAsync(LoginDTO loginDTO);
        Task LogoutAsync();
    }
}
