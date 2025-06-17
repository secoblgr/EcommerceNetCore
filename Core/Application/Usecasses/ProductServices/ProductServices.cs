using Application.Dtos.ProductDtos;
using Application.Interfaces;
using Application.Interfaces.IProductsRepository;
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
        private readonly IProductsRepository _productsrepository;


        public ProductServices(IRepository<Product> repository, IProductsRepository productsrepository)
        {
            _repository = repository;
            _productsrepository = productsrepository;
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

        public async Task<List<ResultProductDto>> GetByProductCategory(int categoryId)
        {
            var products = await _productsrepository.GetProductsByCategory(categoryId);

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

        public async Task<List<ResultProductDto>> GetProductsByPrice(decimal min, decimal max)
        {
            var products = await _productsrepository.GetProductsByPriceFilter(min, max);
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

        public async Task<List<ResultProductDto>> GetProductsSearch(string searchTerm)
        {
            var products = await _productsrepository.SearchProductsByNameAsync(searchTerm);
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

        public async Task<List<ResultProductDto>> GetProductsSortedByPrice(string sortOrder)
        {
            var products = await _productsrepository.GetProductsSortedByPriceAsync(sortOrder);
            return products.Select(p => new ResultProductDto
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                Price = p.Price,
                Stock = p.Stock,
                ImageUrl = p.ImageUrl,
                Description = p.Description,
                CategoryId = p.CategoryId
            }).ToList();
        }

        public async Task<List<ResultProductDto>> GetTakeAsync(int count)            // count kadar listeleme yapmamızı sağlayan metod.
        {
            var products = await _repository.GetProductTakeAsync(count);

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
