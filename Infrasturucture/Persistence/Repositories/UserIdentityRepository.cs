using Application.Dtos.AccountDtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Persistence.Context.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UserIdentityRepository : IUserIdentityRepository
    {
        private readonly UserManager<AppIdentityUser> _userManager;
        private readonly SignInManager<AppIdentityUser> _signInManager;
        public UserIdentityRepository(UserManager<AppIdentityUser> userManager, SignInManager<AppIdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<string> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                return "User Not Found !";
            }

            var result = await _signInManager.PasswordSignInAsync(dto.Email, dto.Password,true,false);

            if (result.Succeeded)
            {
                return "Login Succesfully !";
            }
            if (result.IsLockedOut)
            {
                return  "User Locked !";
            }
            if (result.IsNotAllowed)
            {
                return "No Entry Allowed !";
            }
            if (result.RequiresTwoFactor)
            {
                return "Verification Required!";
            }
            return "Email or Password Wrong !";
        }

        public async Task<string> RegisterAsync(RegisterDto dto)
        {
            if (dto.Password != dto.RePassword)
            {
                return "Şifreler uyumlu değil !";
            }
            var user = new AppIdentityUser
            {
                FirstName = dto.Name,
                LastName = dto.Surname,
                UserName = dto.Email,
                Email = dto.Email,
                PhoneNumber = dto.Phone,
            };
            var result = await _userManager.CreateAsync(user,dto.Password);
            if (result.Succeeded)
            {
                return "Kayıt Başarılı!";
            }
            else
            {
                return result.Errors.ToString();
            }
        }
        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
        public Task<string> ChangePasswordAsync()
        {
            throw new NotImplementedException();
        }

    }
}
