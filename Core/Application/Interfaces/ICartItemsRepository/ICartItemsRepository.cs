using Application.Dtos.CartItemDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.ICartItemsRepository
{
    public interface ICartItemsRepository
    {
        Task UpdateQuantityAsync(int cartId,int productId,int quantity);
        Task<bool> CheckCartItemAsync(int cartId, int productId);

        Task UpdateQuantityOnCartAsync(UpdateCartItemDto model);
    }
}
