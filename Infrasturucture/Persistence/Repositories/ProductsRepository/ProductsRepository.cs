using Application.Dtos.ProductDtos;
using Application.Interfaces.IProductsRepository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.ProductsRepository
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly AppDbContext _context;

        public ProductsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetProductsByCategory(int categoryId)
        {
            return await _context.Products.Where(x => x.CategoryId == categoryId).ToListAsync();
        }

        public async Task<List<Product>> GetProductsByPriceFilter(decimal minPrice, decimal maxPrice)
        {
            return await _context.Products.Where(x => x.Price >= minPrice && x.Price <= maxPrice).ToListAsync();
        }

        public async Task<List<Product>> GetProductsSortedByPriceAsync(string sortOrder)
        {
            IQueryable<Product> query = _context.Products;

            query = sortOrder switch
            {
                "price_asc" => query.OrderBy(p => p.Price),
                "price_desc" => query.OrderByDescending(p => p.Price),
                _ => query
            };

            return await query.ToListAsync();
        }

        public async Task<List<Product>> SearchProductsByNameAsync(string searchTerm)
        {
            return await _context.Products.Where(x => x.ProductName.Contains(searchTerm) || x.Description.Contains(searchTerm)).ToListAsync();
        }
    }
}

