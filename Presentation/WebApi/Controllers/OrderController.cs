using Application.Dtos.OrderDtos;
using Application.Usecasses.OrderServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices _orderServices;
        public OrderController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrder()
        {
            var order = await _orderServices.GetAllOrderAsync();
            return Ok(order);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdOrder(int id)
        {
            var order = await _orderServices.GetByIdOrderAsync(id);
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDto dto)
        {
            await _orderServices.CreateOrderAsync(dto);
            return Ok("Order successfully created!");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrder(UpdateOrderDto dto)
        {
            await _orderServices.UpdateOrderAsync(dto);
            return Ok("Order successfully updated!");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _orderServices.DeleteOrderAsync(id);
            return Ok("Order successfully deleted!");
        }
    }
}
