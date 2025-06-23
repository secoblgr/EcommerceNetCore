using Application.Dtos.AccountDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
   public  interface IUserIdentityRepository
    {
        Task<string> LoginAsync(LoginDto dto);
        Task<string> RegisterAsync(RegisterDto dto);
        Task LogoutAsync();
        Task<string> ChangePasswordAsync();              // daha sonra oluşturulacak.
    }
}
