using Application.Dtos.CartItemDtos;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Usecasses.CartItemServices
{
    public class CartItemServices : ICartItemServices
    {
        private readonly IRepository<CartItem> _repository;

        public CartItemServices(IRepository<CartItem> repository)
        {
            _repository = repository;
        }

        public async Task CreateCartItemAsync(CreateCartItemDto model)
        {
            await _repository.CreateAsync(new CartItem
            {
                // CartId = model.CartId,
                ProductId = model.ProductId,
                Quantity = model.Quantity,
                TotalPrice = model.TotalPrice,
            });
        }

        public async Task DeleteCartItemAsync(int id)
        {
            var value = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(value);
        }

        public async Task<List<ResultCartItemDto>> GetAllCartItemAsync()
        {
            var cartItems = await _repository.GetAllAsync();

            return cartItems

                .Select(x => new ResultCartItemDto
                {
                    CartId = x.CartId,
                    CartItemId = x.CartItemId,
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    TotalPrice = x.TotalPrice,
                }).ToList();
        }


        public async Task<GetByIdCartItemDto> GetByIdCartItemAsync(int id)
        {
            var cartItem = await _repository.GetByIdAsync(id);
            return new GetByIdCartItemDto
            {
                Quantity = cartItem.Quantity,
                ProductId = cartItem.ProductId,
                CartId = cartItem.CartId,
                TotalPrice = cartItem.TotalPrice,
                CartItemId = cartItem.CartItemId,
            };
        }

        public async Task UpdateCartItemAsync(UpdateCartItemDto model)
        {
            var cartItem = await _repository.GetByIdAsync(model.CartItemId);
            cartItem.Quantity = model.Quantity;
            cartItem.TotalPrice = model.TotalPrice;
            cartItem.ProductId = model.ProductId;
            cartItem.CartId = model.CartId;
            await _repository.UpdateAsync(cartItem);
        }


    }
}
