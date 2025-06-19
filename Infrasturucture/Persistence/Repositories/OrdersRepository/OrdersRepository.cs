
using Application.Dtos.TownDtos;
using Application.Interfaces.IOrdersRepository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.OrdersRepository
{
    public class OrdersRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrdersRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<City>> GetCityAsync()
        {
            var city = await _context.City.ToListAsync();
            return city;
        }

        public async Task<List<Town>> GetTownByCityIdAsync(int cityId)
        {
            var town = await _context.Town.Where(x => x.CityId == cityId).ToListAsync();
            return town;
        }
    }
}
