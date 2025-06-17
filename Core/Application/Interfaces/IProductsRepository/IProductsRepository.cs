using Application.Dtos.ProductDtos;
using Application.Usecasses.ProductServices;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IProductsRepository
{
    public interface IProductsRepository 
    {
        Task<List<Product>> GetProductsByCategory(int categoryId);
    }
}
