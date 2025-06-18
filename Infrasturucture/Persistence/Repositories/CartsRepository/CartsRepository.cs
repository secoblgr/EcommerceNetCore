using Application.Interfaces.ICartsRepository;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.CartsRepository
{
    public class CartsRepository : ICartsRepository
    {
        private readonly AppDbContext _context;

        public CartsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task UpdateTotalAmountAsync(int cartId, decimal amount)
        {
            var value = await _context.Carts.Where(x => x.CartId == cartId).FirstOrDefaultAsync();
            if (value != null)
            {
                value.TotalAmount = amount;
                await _context.SaveChangesAsync();
            }
        }
    }
}
