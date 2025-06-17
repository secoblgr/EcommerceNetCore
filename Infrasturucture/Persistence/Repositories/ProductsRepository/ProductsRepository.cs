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
    }
}

