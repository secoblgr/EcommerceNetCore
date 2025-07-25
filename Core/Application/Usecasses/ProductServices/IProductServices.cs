﻿using Application.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Usecasses.ProductServices
{
    public interface IProductServices
    {
        Task<List<ResultProductDto>> GetAllProductAsync();
        Task<GetByIdProductDto> GetByIdProductAsync(int id);
        Task CreateProductAsync(CreateProductDto model);
        Task UpdateProductAsync(UpdateProductDto model);
        Task DeleteProductAsync(int id);
        Task<List<ResultProductDto>> GetTakeAsync(int count);
        Task<List<ResultProductDto>> GetByProductCategory(int category);
        Task<List<ResultProductDto>> GetProductsByPrice(decimal min, decimal max);
        Task<List<ResultProductDto>> GetProductsSortedByPrice(string sortOrder);
        Task<List<ResultProductDto>> GetProductsSearch(string searchTerm);

    }
}
