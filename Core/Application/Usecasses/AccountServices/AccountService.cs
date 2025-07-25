﻿using Application.Dtos.AccountDtos;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Usecasses.AccountServices
{
    public class AccountService : IAccountService
    {

        private readonly IUserIdentityRepository _userIdentityRepository;

        public AccountService(IUserIdentityRepository userIdentityRepository)
        {
            _userIdentityRepository = userIdentityRepository;
        }

        public async Task<string> Login(LoginDto dto)
        {
            var result = await _userIdentityRepository.LoginAsync(dto);
            return result;
        }

        public async Task Logout()
        {
            await _userIdentityRepository.LogoutAsync();
        }
        public async Task<string> Register(RegisterDto dto)
        {
            var result = await _userIdentityRepository.RegisterAsync(dto);
            return result;
        }
        public Task<string> ChangePassword()
        {
            throw new NotImplementedException();
        }
    }
}
