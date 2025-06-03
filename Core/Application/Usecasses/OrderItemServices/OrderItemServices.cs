using Application.Dtos.OrderItemDtos;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Usecasses.OrderItemServices
{
    public class OrderItemServices : IOrderItemServices
    {
        private readonly IRepository<OrderItem> _repository;

        public OrderItemServices(IRepository<OrderItem> repository)
        {
            _repository = repository;
        }

        public async Task CreateOrderItemAsync(CreateOrderItemDto model)
        {
            await _repository.CreateAsync(new OrderItem
            {
                //  OrderId = model.OrderId,
                ProductId = model.ProductId,
                Quantity = model.Quantity,
                Price = model.Price,
            });
        }

        public async Task DeleteOrderItemAsync(int id)
        {
            var value = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(value);
        }

        public async Task<List<ResultOrderItemDto>> GetAllOrderItemAsync()
        {
            var orderItems = await _repository.GetAllAsync() ?? new List<OrderItem>();

            return orderItems
                .Where(x => x != null)
                .Select(x => new ResultOrderItemDto
                {
                    OrderId = x.OrderId,
                    OrderItemId = x.OrderItemId,
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    Price = x.Price,
                }).ToList();
        }


        public async Task<GetByIdOrderItemDto> GetByIdOrderItemAsync(int id)
        {
            var orderItem = await _repository.GetByIdAsync(id);
            return new GetByIdOrderItemDto
            {
                ProductId = orderItem.ProductId,
                OrderId = orderItem.OrderId,
                Quantity = orderItem.Quantity,
                Price = orderItem.Price,
                OrderItemId = orderItem.OrderItemId,
            };
        }

        public async Task UpdateOrderItemAsync(UpdateOrderItemDto model)
        {
            var orderItem = await _repository.GetByIdAsync(model.OrderItemId);
            orderItem.OrderItemId = model.OrderItemId;
            orderItem.OrderId = model.OrderId;
            orderItem.ProductId = model.ProductId;
            orderItem.Quantity = model.Quantity;
            orderItem.Price = model.Price;
            await _repository.UpdateAsync(orderItem);
        }
    }
}
