using Application.Dtos.CityDtos;
using Application.Dtos.TownDtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IOrdersRepository
{
   public  interface IOrderRepository
    {
        Task<List<City>> GetCityAsync();
        Task<List<Town>> GetTownByCityIdAsync(int cityId);
    }
}
