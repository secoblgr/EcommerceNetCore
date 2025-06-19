using Application.Dtos.CartDtos;
using Application.Dtos.CartItemDtos;
using Application.Dtos.ProductDtos;
using Application.Interfaces;
using Application.Interfaces.ICartsRepository;
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
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Product> _productRepositorty;
        private readonly ICartsRepository _cartsRepository;

        public CartServices(IRepository<CartItem> cartItemRepository, IRepository<Cart> repository, IRepository<Customer> customerRepository, IRepository<Product> productRepositorty, ICartsRepository cartsRepository)
        {
            _cartItemRepository = cartItemRepository;
            _repository = repository;
            _customerRepository = customerRepository;
            _productRepositorty = productRepositorty;
            _cartsRepository = cartsRepository;
        }

        public async Task CreateCartAsync(CreateCartDto model)
        {
            var cart = new Cart
            {
                //  TotalAmount = model.TotalAmount,
                CreatedDate = DateTime.Now,
                CustomerId = model.CustomerId
            };
            await _repository.CreateAsync(cart);

            var sum = 0;
            foreach (var item in model.CartItems)
            {
                var cartItem = new CartItem
                {
                    CartId = cart.CartId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    TotalPrice = item.TotalPrice,
                };
                sum = sum + (item.Quantity * item.TotalPrice);
                await _cartItemRepository.CreateAsync(cartItem);
            }
            cart.TotalAmount = sum;
            await _repository.UpdateAsync(cart);
        }

        public async Task DeleteCartAsync(int id)
        {
            var cart = await _repository.GetByIdAsync(id);                         //cartı silme işlmei
            var cartItems = await _cartItemRepository.GetAllAsync();               // cart silinmiş ise cart itemları da sil.
            foreach (var item in cartItems)
            {
                if (item.CartId == id)
                {
                    var cartitem = await _cartItemRepository.GetByIdAsync(item.CartItemId);
                    await _cartItemRepository.DeleteAsync(cartitem);
                }
            }
            await _repository.DeleteAsync(cart);
        }

        public async Task<List<ResultCartDto>> GetAllCartAsync()
        {
            var cart = await _repository.GetAllAsync();
            var cartitem = await _cartItemRepository.GetAllAsync();
            var product = await _productRepositorty.GetAllAsync();
            var result = new List<ResultCartDto>();

            foreach (var item in cart)
            {
                var customerDto = await _customerRepository.GetByFilterAsync(cus => cus.CustomerId == item.CustomerId);
                var cartdto = new ResultCartDto
                {
                    CartId = item.CartId,
                    CreatedDate = item.CreatedDate,
                    CustomerId = item.CustomerId,
                    TotalAmount = item.TotalAmount,
                    Customer = customerDto,
                    CartItems = new List<ResultCartItemDto>()
                };

                foreach (var item1 in item.CartItems)
                {
                    var productDto = await _productRepositorty.GetByFilterAsync(prd => prd.ProductId == item1.ProductId);
                    var cartItemDto = new ResultCartItemDto
                    {
                        CartId = item1.CartId,
                        CartItemId = item1.CartItemId,
                        ProductId = item1.ProductId,
                        Quantity = item1.Quantity,
                        TotalPrice = item1.TotalPrice,
                        Product = productDto,
                    };
                    cartdto.CartItems.Add(cartItemDto);
                }
                result.Add(cartdto);
            }
            return result;
        }


        public async Task<GetByIdCartDto> GetByIdCartAsync(int id)
        {
            var cart = await _repository.GetByIdAsync(id);
            var customer = await _customerRepository.GetByIdAsync(id);
            var cartItem = await _cartItemRepository.GetAllAsync();

            var result = new GetByIdCartDto
            {
                CartId = cart.CartId,
                CreatedDate = cart.CreatedDate,
                CustomerId = cart.CustomerId,
                CartItems = new List<ResultCartItemDto>(),
                TotalAmount = cart.TotalAmount,
                Customer = customer,
            };
            if (cart.CartItems != null)
            {
                foreach (var item in cart.CartItems)
                {
                    var productDto = await _productRepositorty.GetByFilterAsync(prd => prd.ProductId == item.ProductId);
                    var cartItemDto = new ResultCartItemDto
                    {
                        CartId = item.CartId,
                        CartItemId = item.CartItemId,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        TotalPrice = item.TotalPrice,
                        Product = productDto,
                    };
                    result.CartItems.Add(cartItemDto);
                }
            }

            return result;
        }

        public async Task UpdateCartAsync(UpdateCartDto model)
        {
            var cart = await _repository.GetByIdAsync(model.CartId);
            var cartItems = await _cartItemRepository.GetAllAsync();                //cart item null dönmemesi için burda çagırıyoruz yapıyoruz.
            var sum = 0;
            foreach (var item1 in model.CartItems)
            {
                foreach (var item in cart.CartItems)
                {
                    var cartItem = await _cartItemRepository.GetByIdAsync(item.CartItemId);

                    if (item.CartItemId == item1.CartItemId)
                    {
                        cartItem.Quantity = item1.Quantity;
                        cartItem.TotalPrice = item1.TotalPrice;
                    }
                    sum = sum + item.TotalPrice;
                }
            }
            cart.TotalAmount = sum;
            await _repository.UpdateAsync(cart);
        }

        public async Task UpdateTotalAmount(int cartId, decimal totalAmount)
        {
            await _cartsRepository.UpdateTotalAmountAsync(cartId, totalAmount);
        }
    }
}

