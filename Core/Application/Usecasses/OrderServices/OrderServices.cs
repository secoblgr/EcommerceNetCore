using Application.Dtos.OrderDtos;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Usecasses.OrderServices
{
    public class OrderServices : IOrderServices
    {
        private readonly IRepository<Order> _repository;                           // çalışacagımız modele bağlantı .

        public OrderServices(IRepository<Order> repository)
        {
            _repository = repository;
        }

        public async Task CreateOrderAsync(CreateOrderDto model)
        {
            await _repository.CreateAsync(new Order
            {
                OrderDate = model.OrderDate,
                TotalAmount = model.TotalAmount,
                ShippingAddress = model.ShippingAddress,
                OrderStatus = model.OrderStatus,
                CustomerId = model.CustomerId,
            });
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(order);
        }

        public async Task<List<ResultOrderDto>> GetAllOrderAsync()
        {
            var order = await _repository.GetAllAsync();
            return order.Select(x => new ResultOrderDto
            {
                OrderId = x.OrderId,
             //   Customer = x.Customer,
                OrderDate = x.OrderDate,
                TotalAmount = x.TotalAmount,
                ShippingAddress = x.ShippingAddress,
                OrderStatus = x.OrderStatus,
             //   OrderItems = x.OrderItems,
                CustomerId = x.CustomerId,
            }).ToList();
        }

        public async Task<GetByIdOrderDto> GetByIdOrderAsync(int id)
        {
            var order = await _repository.GetByIdAsync(id);
            return new GetByIdOrderDto
            {
                OrderId = order.OrderId,
               // Customer = order.Customer,
                CustomerId = order.CustomerId,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                ShippingAddress = order.ShippingAddress,
                OrderStatus = order.OrderStatus,
               // OrderItems = order.OrderItems,
            };
        }

        public async Task UpdateOrderAsync(UpdateOrderDto model)
        {
            var order = await _repository.GetByIdAsync(model.OrderId);
            order.OrderDate = model.OrderDate;
            order.TotalAmount = model.TotalAmount;
            order.OrderStatus = model.OrderStatus;
            order.ShippingAddress = model.ShippingAddress;
            order.CustomerId = model.CustomerId;
            await _repository.UpdateAsync(order);
        }
    }
}
