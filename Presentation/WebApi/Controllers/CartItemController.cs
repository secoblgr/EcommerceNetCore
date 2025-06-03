using Application.Dtos.CartItemDtos;
using Application.Usecasses.CartItemServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly ICartItemServices _cartItemServices;

        public CartItemController(ICartItemServices cartItemServices)
        {
            _cartItemServices = cartItemServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCartItem()
        {
            var cartItems = await _cartItemServices.GetAllCartItemAsync();
            return Ok(cartItems);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdCartItem(int id)
        {
            var cartItem = await _cartItemServices.GetByIdCartItemAsync(id);
            return Ok(cartItem);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCartItem(CreateCartItemDto dto)
        {
            await _cartItemServices.CreateCartItemAsync(dto);
            return Ok("Cart item successfully created!");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCartItem(UpdateCartItemDto dto)
        {
            await _cartItemServices.UpdateCartItemAsync(dto);
            return Ok("Cart item successfully updated!");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            await _cartItemServices.DeleteCartItemAsync(id);
            return Ok("Cart item successfully deleted!");
        }
    }
}
