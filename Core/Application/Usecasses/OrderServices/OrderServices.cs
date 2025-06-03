using Application.Dtos.OrderDtos;
using Application.Dtos.OrderItemDtos;
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
        private readonly IRepository<OrderItem> _orderItemRepository;


        public OrderServices(IRepository<Order> repository, IRepository<OrderItem> orderItemRepository)
        {
            _repository = repository;
            _orderItemRepository = orderItemRepository;
        }

        public async Task CreateOrderAsync(CreateOrderDto model)
        {
            var order = new Order
            {
                OrderDate = model.OrderDate,
                TotalAmount = model.TotalAmount,
                ShippingAddress = model.ShippingAddress,
                OrderStatus = model.OrderStatus,
                CustomerId = model.CustomerId,
            };
            await _repository.CreateAsync(order);
            foreach (var item in model.OrderItems)
            {
                await _orderItemRepository.CreateAsync(new OrderItem
                {
                    OrderId = order.OrderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price,
                });
            }
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(order);
        }

        public async Task<List<ResultOrderDto>> GetAllOrderAsync()
        {
            var orders = await _repository.GetAllAsync();
            var orderitems = await _orderItemRepository.GetAllAsync();

            return orders.Select(x => new ResultOrderDto
            {
                OrderId = x.OrderId,
                OrderDate = x.OrderDate,
                TotalAmount = x.TotalAmount,
                ShippingAddress = x.ShippingAddress,
                OrderStatus = x.OrderStatus,
                CustomerId = x.CustomerId,
                OrderItems = x.OrderItems.Select(oi => new ResultOrderItemDto
                {
                    OrderId = oi.OrderId,
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity,
                    Price = oi.Price,
                    OrderItemId = oi.OrderItemId,
                }).ToList()
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
                //OrderItems = order.OrderItems,
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

