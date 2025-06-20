using Application.Dtos.CityDtos;
using Application.Dtos.OrderDtos;
using Application.Dtos.OrderItemDtos;
using Application.Dtos.TownDtos;
using Application.Interfaces;
using Application.Interfaces.IOrdersRepository;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        private readonly IOrderRepository _orderRepository;



        public OrderServices(IRepository<Order> repository, IRepository<OrderItem> orderItemRepository, IRepository<Customer> customerRepository, IRepository<Product> productRepository, IOrderRepository orderRepository)
        {
            _repository = repository;
            _orderItemRepository = orderItemRepository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        public async Task CreateOrderAsync(CreateOrderDto model)
        {
            decimal sum = 0;
            var order = new Order
            {
                OrderDate = DateTime.Now,
                TotalAmount = sum,
                ShippingAddress = model.ShippingAddress,
                OrderStatus = model.OrderStatus,
                CustomerId = model.CustomerId,
                CargoCityId = model.CargoCityId,
                CargoTownId = model.CargoTownId,
                CustomerName = model.CustomerName,
                CustomerSurname =model.CustomerSurname,
                CustomerPhone = model.CustomerPhone,
                CustomerEmail = model.CustomerEmail,
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

        public async Task<List<ResultCityDto>> GetAllCity()
        {
            var city = await _orderRepository.GetCityAsync();
            return city.Select(x => new ResultCityDto
            {
                CityId = x.CityId,
                CityName = x.CityName,
            }).ToList();
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
                    CargoCityId = item.CargoCityId,
                    CargoTownId = item.CargoTownId,
                    CustomerName = item.CustomerName,
                    CustomerSurname = item.CustomerSurname,
                    CustomerPhone = item.CustomerPhone,
                    CustomerEmail = item.CustomerEmail,
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
                OrderStatus = order.OrderStatus,
                CustomerId = order.CustomerId,
                CargoCityId = order.CargoCityId,
                CargoTownId = order.CargoTownId,
                CustomerName = order.CustomerName,
                CustomerSurname = order.CustomerSurname,
                CustomerPhone = order.CustomerPhone,
                CustomerEmail = order.CustomerEmail,
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

        public async Task<List<ResultTownDto>> GetTownByCityId(int id)
        {
            var town = await _orderRepository.GetTownByCityIdAsync(id);
            return town.Select(x => new ResultTownDto
            {
                Id = x.Id,
                CityId = x.CityId,
                TownId = x.TownId,
                TownName = x.TownName,
            }).ToList();
        }
    }
}

