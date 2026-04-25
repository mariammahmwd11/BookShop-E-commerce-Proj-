using BookSAW.BL.DTO;
using BookSAW.BL.IServices;
using BookSAW.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace BookSAW.BL.Services
{
    public class AccountService : IAccountService
    {
        private readonly SignInManager<AppUser> signInManager;
        private readonly UserManager<AppUser> userManager;

        public AccountService( SignInManager<AppUser> signInManager,UserManager<AppUser> userManager) {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public async Task<AuthResult> LoginAsync(LoginDTO loginDTO)
        {
            AuthResult authResult = new AuthResult();

            var user= await userManager.FindByEmailAsync(loginDTO.Email);
            if(user!=null) 
                {
                var validpassword = await userManager.CheckPasswordAsync(user, loginDTO.Password);
                if (validpassword == true)
                {
                    await signInManager.SignInAsync(user, loginDTO.RememberMe);
                    authResult.IsSuccess = true;
                    authResult.Message = "Login Successfully";
                }
                else
                {
                    authResult.IsSuccess = false;
                    authResult.Message = "Wrong Email or PassWord";
                }
            
            }
            else
            {
                authResult.IsSuccess = false;
                authResult.Message = "There is no User with this Email ";
            }
            return authResult;
        }

        public async Task LogoutAsync()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<AuthResult> RegisterAsync(RegisterDTO registerDTO)
        {
            AuthResult result = new AuthResult();

            var existingUser =  await userManager.FindByEmailAsync(registerDTO.Email);
            if (existingUser != null)
            {
                result.IsSuccess = false;
                result.Message = "Email already exists";
                return result;
            }
            var newUser = new AppUser
            {
                UserName = registerDTO.UserName,
                Email = registerDTO.Email

            };
            var Result =await userManager.CreateAsync(newUser, registerDTO.Password);
            if (Result.Succeeded)
            {
                await signInManager.SignInAsync(newUser, false);
                result.IsSuccess = true;
                result.Message = "Registerd Successfully";
            }
            return result;
        }
    }
}
