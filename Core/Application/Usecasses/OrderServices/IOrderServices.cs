﻿using Application.Dtos.CustomerDtos;
using Application.Dtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Usecasses.OrderServices
{
    public interface IOrderServices
    {
        Task<List<ResultOrderDto>> GetAllOrderAsync();
        Task<GetByIdOrderDto> GetByIdOrderAsync(int id);
        Task CreateOrderAsync(CreateOrderDto model);
        Task UpdateOrderAsync(UpdateOrderDto model);
        Task DeleteOrderAsync(int id);
    }
}
