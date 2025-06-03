using Application.Dtos.CartDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Usecasses.CartServices
{
   public interface ICartServices
    {
        Task<List<ResultCartDto>> GetAllCartAsync();                          
        Task<GetByIdCartDto> GetByIdCartAsync(int id);                        
        Task CreateCartAsync(CreateCartDto model);        
        Task UpdateCartAsync(UpdateCartDto model);         
        Task DeleteCartAsync(int id);
    }
}
