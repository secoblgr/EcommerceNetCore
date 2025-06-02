using Application.Dtos.CustomerDtos;
using Application.Usecasses.CustomerServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerServices _customerServices;

        public CustomerController(ICustomerServices customerServices)
        {
            _customerServices = customerServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomer()
        {
            var customer = await _customerServices.GetAllCustomerAsync();
            return Ok(customer);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdCustomer(int id)
        {
            var customer = await _customerServices.GetByIdCustomerAsync(id);
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CreateCustomerDto dto)
        {
            await _customerServices.CreateCustomerAsync(dto);
            return Ok("Successfully created customer!");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCustomer(UpdateCustomerDto dto)
        {
            await _customerServices.UpdateCustomerAsync(dto);
            return Ok("Successfully updated customer!");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await _customerServices.DeleteCustomerAsync(id);
            return Ok("Succesfully customer deleted!");
        }
    }
}
