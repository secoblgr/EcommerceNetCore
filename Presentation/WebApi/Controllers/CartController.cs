using Application.Dtos.CartDtos;
using Application.Usecasses.CartServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartServices _cartServices;

        public CartController(ICartServices cartServices)
        {
            _cartServices = cartServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCart()
        {
            var cart = await _cartServices.GetAllCartAsync();
            return Ok(cart);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdCart(int id)
        {
            var cart = await _cartServices.GetByIdCartAsync(id);
            return Ok(cart);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCart(CreateCartDto dto)
        {
            await _cartServices.CreateCartAsync(dto);
            return Ok("Cart successfully created!");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCart(UpdateCartDto dto)
        {
            await _cartServices.UpdateCartAsync(dto);
            return Ok("Cart successfully updated!");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCart(int id)
        {
            await _cartServices.DeleteCartAsync(id);
            return Ok("Cart successfully deleted!");
        }
    }
}
