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

        public Task<string> LoginAsync(LoginDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<string> RegisterAsync(RegisterDto dto)
        {
            throw new NotImplementedException();
        }
        public Task LogoutAsync()
        {
            throw new NotImplementedException();
        }
        public Task<string> ChangePasswordAsync()
        {
            throw new NotImplementedException();
        }

    }
}
