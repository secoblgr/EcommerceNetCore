using Application.Dtos.CustomerDtos;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Usecasses.CustomerServices
{
    public class CustomerServices : ICustomerServices
    {
        private readonly IRepository<Customer> _repository;                           // çalışacagımız modele bağlantı .

        public CustomerServices(IRepository<Customer> repository)
        {
            _repository = repository;
        }

        public async Task CreateCustomerAsync(CreateCustomerDto model)
        {
            await _repository.CreateAsync(
                new Customer
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                }
            );
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var customer = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(customer);
        }

        public async Task<List<ResultCustomerDto>> GetAllCustomerAsync()
        {
            var customers = await _repository.GetAllAsync();
            return customers.Select(x => new ResultCustomerDto
            {
                CustomerId = x.CustomerId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
             //   Order = x.Order
            }).ToList();
        }

        public async Task<GetByIdCustomerDto> GetByIdCustomerAsync(int id)
        {
            var customer = await _repository.GetByIdAsync(id);
            return new GetByIdCustomerDto
            {
                CustomerId = customer.CustomerId,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
              //  Order = customer.Order,
            };
        }

        public async Task UpdateCustomerAsync(UpdateCustomerDto model)
        {
            var customer = await _repository.GetByIdAsync(model.CustomerId);
            customer.CustomerId = model.CustomerId;
            customer.FirstName = model.FirstName;
            customer.LastName = model.LastName;
            customer.Email = model.Email;
            await _repository.UpdateAsync(customer);
        }
    }
}
