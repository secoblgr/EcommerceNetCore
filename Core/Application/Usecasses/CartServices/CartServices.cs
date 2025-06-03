using Application.Dtos.CartDtos;
using Application.Dtos.CartItemDtos;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Usecasses.CartServices
{
    public class CartServices : ICartServices
    {
        private readonly IRepository<Cart> _repository;
        private readonly IRepository<CartItem> _cartItemRepository;

        public CartServices(IRepository<CartItem> cartItemRepository, IRepository<Cart> repository)
        {
            _cartItemRepository = cartItemRepository;
            _repository = repository;

        }

        public async Task CreateCartAsync(CreateCartDto model)
        {
            var cart = new Cart
            {
                TotalAmount = model.TotalAmount,
                CreatedDate = DateTime.Now,
                CartItems = model.CartItems,
                CustomerId = model.CustomerId
            };
            await _repository.CreateAsync(cart);
            foreach (var item in model.CartItems)
            {
                await _cartItemRepository.CreateAsync(new CartItem
                {
                    CartId = cart.CartId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    TotalPrice = item.TotalPrice,
                });
            }
        }

        public async Task DeleteCartAsync(int id)
        {
            var cart = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(cart);
        }

        public async Task<List<ResultCartDto>> GetAllCartAsync()
        {
            var cart = await _repository.GetAllAsync();
            var cartitem = await _cartItemRepository.GetAllAsync(); 

            return cart.Select(x => new ResultCartDto
            {
                CartId = x.CartId,
                CreatedDate = x.CreatedDate,
                CustomerId = x.CustomerId,
                TotalAmount = x.TotalAmount,
                CartItems = x.CartItems.Select(ci => new ResultCartItemDto
                {
                    CartId = ci.CartId,
                    CartItemId = ci.CartItemId,
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    TotalPrice = ci.TotalPrice,
                }).ToList()
            }).ToList();
        }


        public async Task<GetByIdCartDto> GetByIdCartAsync(int id)
        {
            var cart = await _repository.GetByIdAsync(id);
            return new GetByIdCartDto
            {
                CartId = cart.CartId,
                CreatedDate = cart.CreatedDate,
                CustomerId = cart.CustomerId,
                TotalAmount = cart.TotalAmount,
            };
        }

        public async Task UpdateCartAsync(UpdateCartDto model)
        {
            var cart = await _repository.GetByIdAsync(model.CartId);
            cart.CreatedDate = model.CreatedDate;
            cart.CustomerId = model.CustomerId;
            cart.TotalAmount = model.TotalAmount;
            await _repository.UpdateAsync(cart);
        }
    }
}
