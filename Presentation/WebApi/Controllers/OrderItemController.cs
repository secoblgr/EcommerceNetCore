using Application.Dtos.OrderDtos;
using Application.Dtos.OrderItemDtos;
using Application.Usecasses.OrderItemServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemServices _orderItemServices;

        public OrderItemController(IOrderItemServices orderItemServices)
        {
            _orderItemServices = orderItemServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrderItem()
        {
            var orderItem = await _orderItemServices.GetAllOrderItemAsync();
            return Ok(orderItem);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdOrderItem(int id)
        {
            var orderItem = await _orderItemServices.GetByIdOrderItemAsync(id);
            return Ok(orderItem);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderItem(CreateOrderItemDto dto)
        {
            await _orderItemServices.CreateOrderItemAsync(dto);
            return Ok("OrderItem successfully created!");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrder(UpdateOrderItemDto dto)
        {
            await _orderItemServices.UpdateOrderItemAsync(dto);
            return Ok("OrderItem successfully updated!");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _orderItemServices.DeleteOrderItemAsync(id);
            return Ok("OrderItem successfully deleted!");
        }
    }
}
