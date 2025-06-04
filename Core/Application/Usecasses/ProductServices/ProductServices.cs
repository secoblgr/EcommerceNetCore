using Application.Dtos.ProductDtos;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Usecasses.ProductServices
{
    public class ProductServices : IProductServices
    {
        private readonly IRepository<Product> _repository;

        public ProductServices(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task CreateProductAsync(CreateProductDto model)
        {
            await _repository.CreateAsync(new Product
            {
                ProductName = model.ProductName,
                Description = model.Description,
                Price = model.Price,
                Stock = model.Stock,
                ImageUrl = model.ImageUrl,
                CategoryId = model.CategoryId
            });
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(product);
        }

        public async Task<List<ResultProductDto>> GetAllProductAsync()
        {
            var products = await _repository.GetAllAsync();

            return products.Select(x => new ResultProductDto
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                Description = x.Description,
                Price = x.Price,
                Stock = x.Stock,
                ImageUrl = x.ImageUrl,
                CategoryId = x.CategoryId
            }).ToList();
        }


        public async Task<GetByIdProductDto> GetByIdProductAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            return new GetByIdProductDto
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                ImageUrl = product.ImageUrl,
                CategoryId = product.CategoryId,
            };
        }

        public async Task UpdateProductAsync(UpdateProductDto model)
        {
            var product = await _repository.GetByIdAsync(model.ProductId);

            product.ProductName = model.ProductName;
            product.Description = model.Description;
            product.Price = model.Price;
            product.Stock = model.Stock;
            product.ImageUrl = model.ImageUrl;
            product.CategoryId = model.CategoryId;
            await _repository.UpdateAsync(product);
        }
    }
}
