using Application.Dtos.SubscriberDtos;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Usecasses.SubscriberServices
{
    public class SubscriberServices : ISubscriberServices
    {
        private readonly IRepository<Subscriber> _repository;

        public SubscriberServices(IRepository<Subscriber> repository)
        {
            _repository = repository;
        }

        public async Task CreateSubscriberAsync(CreateSubscriberDto dto)
        {
            var value = new Subscriber()
            {
                Email = dto.Email,
                Name = dto.Name,
                SubscribeDate = dto.SubscribeDate,
            };
            await _repository.CreateAsync(value);
        }

        public async Task DeleteSubscriberAsync(int id)
        {
            var value = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(value);
        }

        public async Task<List<ResultSubscriberDto>> GetAllSubscriberAsync()
        {
            var value = await _repository.GetAllAsync();
            return value.Select(x => new ResultSubscriberDto
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                SubscribeDate = x.SubscribeDate,
            }).ToList();
        }

        public async Task<GetByIdSubscriberDto> GetByIdSubscriberAsync(int id)
        {
            var value = await _repository.GetByIdAsync(id);
            return new GetByIdSubscriberDto
            {
                Id = value.Id,
                Email = value.Email,
                Name = value.Name,
                SubscribeDate = value.SubscribeDate,
            };
        }

        public async Task UpdateSubscriberAsync(UpdateSubscriberDto dto)
        {
            var value = await _repository.GetByIdAsync(dto.Id);
            value.Name = dto.Name;
            value.Email = dto.Email;
            value.SubscribeDate = dto.SubscribeDate;

            await _repository.UpdateAsync(value);
        }
    }
}
