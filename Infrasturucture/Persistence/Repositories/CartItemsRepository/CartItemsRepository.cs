using Application.Dtos.CartItemDtos;
using Application.Interfaces.ICartItemsRepository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.CartItemsRepository
{
    public class CartItemsRepository : ICartItemsRepository
    {
        private readonly AppDbContext _context;

        public CartItemsRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CheckCartItemAsync(int cartId, int productId)
        {
            var items = await _context.CartItems.Where(x => x.CartId == cartId && x.ProductId == productId).SingleOrDefaultAsync();
            if (items == null)
            {
                return false;
            }
            return true;
        }

        public async Task UpdateQuantityAsync(int cartId, int productId, int quantity)
        {
            var cart = await _context.CartItems.Where(x => x.CartId == cartId && x.ProductId == productId).SingleOrDefaultAsync();
            if (cart != null)
            {
                var tempPrice = cart.TotalPrice / cart.Quantity;
                cart.Quantity += quantity;
                cart.TotalPrice = tempPrice * cart.Quantity;
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateQuantityOnCartAsync(UpdateCartItemDto model)
        {
            var cart = await _context.CartItems.Where(x => x.CartId == model.CartId && x.ProductId == model.ProductId).SingleOrDefaultAsync();
            if (cart != null)
            {
                var tempPrice = cart.TotalPrice / cart.Quantity;
                cart.Quantity += model.Quantity;
                cart.TotalPrice = tempPrice * cart.Quantity;
                await _context.SaveChangesAsync();
            }
        }
    }
}
