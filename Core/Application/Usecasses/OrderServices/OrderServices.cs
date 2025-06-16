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
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Product> _productRepository;


        public OrderServices(IRepository<Order> repository, IRepository<OrderItem> orderItemRepository, IRepository<Customer> customerRepository, IRepository<Product> productRepository)
        {
            _repository = repository;
            _orderItemRepository = orderItemRepository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
        }

        public async Task CreateOrderAsync(CreateOrderDto model)
        {
            decimal sum = 0;
            var order = new Order
            {
                OrderDate = model.OrderDate,
                TotalAmount = sum,
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
                sum = sum + item.Price;
            }
            order.TotalAmount = sum;
            await _repository.UpdateAsync(order);
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await _repository.GetByIdAsync(id);
            foreach (var item in order.OrderItems)
            {
                var orderItem = await _orderItemRepository.GetByIdAsync(item.OrderItemId);
                await _orderItemRepository.DeleteAsync(orderItem);
            }
            await _repository.DeleteAsync(order);
        }

        public async Task<List<ResultOrderDto>> GetAllOrderAsync()
        {
            var orders = await _repository.GetAllAsync();
            var orderitems = await _orderItemRepository.GetAllAsync();
            var result = new List<ResultOrderDto>();
            foreach (var item in orders)
            {
                var orderCustomer = await _customerRepository.GetByIdAsync(item.CustomerId);
                var orderdto = new ResultOrderDto
                {

                    OrderId = item.OrderId,
                    OrderDate = item.OrderDate,
                    TotalAmount = item.TotalAmount,
                    ShippingAddress = item.ShippingAddress,
                    OrderStatus = item.OrderStatus,
                    CustomerId = item.CustomerId,
                    Customer = orderCustomer,
                    OrderItems = new List<ResultOrderItemDto>()
                };
                foreach (var item1 in item.OrderItems)
                {
                    var orderitemproduct = await _productRepository.GetByIdAsync(item1.ProductId);
                    var orderitemdto = new ResultOrderItemDto
                    {
                        OrderId = item1.OrderId,
                        ProductId = item1.ProductId,
                        Quantity = item1.Quantity,
                        Price = item1.Price,
                        OrderItemId = item1.OrderItemId,
                        Product = orderitemproduct,
                    };
                    orderdto.OrderItems.Add(orderitemdto);
                }
                result.Add(orderdto);
            }
            return result;
        }


        public async Task<GetByIdOrderDto> GetByIdOrderAsync(int id)
        {
            var order = await _repository.GetByIdAsync(id);
            var orderCustomer = await _customerRepository.GetByIdAsync(order.CustomerId);
            var result = new GetByIdOrderDto
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                ShippingAddress = order.ShippingAddress,
                Customer = orderCustomer,
                CustomerId = order.CustomerId,
                OrderStatus = order.OrderStatus,
                OrderItems = new List<ResultOrderItemDto>()
            };
            foreach (var item in result.OrderItems)
            {
                var orderItemProduct = await _productRepository.GetByIdAsync(item.ProductId);
                var orderitemdto = new ResultOrderItemDto
                {
                    OrderId = item.OrderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    OrderItemId = item.OrderItemId,
                    Product = orderItemProduct,
                };
                result.OrderItems.Add(orderitemdto);
            }
            return result;
        }
        public async Task UpdateOrderAsync(UpdateOrderDto model)
        {
            var order = await _repository.GetByIdAsync(model.OrderId);
            var orderItems = await _orderItemRepository.GetAllAsync();
            order.OrderStatus = model.OrderStatus;
            decimal sum = 0;

            foreach (var item in model.OrderItems)
            {
                foreach (var item1 in order.OrderItems)
                {
                    var orderItem = await _orderItemRepository.GetByIdAsync(item1.OrderItemId);
                    if (item.OrderItemId == item1.OrderItemId)
                    {
                        orderItem.Quantity = item.Quantity;
                        orderItem.Price = item.Price;
                    }
                    sum = sum + item1.Price;
                }
            }
            order.TotalAmount = sum;
            await _repository.UpdateAsync(order);
        }
    }
}

