using Application.Dtos.CustomerDtos;
using Application.Dtos.ProductDtos;
using Application.Usecasses.ProductServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productServices;

        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            var product = await _productServices.GetAllProductAsync();
            return Ok(product);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdProduct(int id)
        {
            var product = await _productServices.GetByIdProductAsync(id);
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto dto)
        {
            await _productServices.CreateProductAsync(dto);
            return Ok("Successfully created product!");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCustomer(UpdateProductDto dto)
        {
            await _productServices.UpdateProductAsync(dto);
            return Ok("Successfully updated product!");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await _productServices.DeleteProductAsync(id);
            return Ok("Succesfully deleted product!");
        }
    }
}
