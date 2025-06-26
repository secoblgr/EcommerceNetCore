using Application.Dtos.SubscriberDtos;
using Application.Dtos.SupportDtos;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Usecasses.SupportServices
{
    public class SupportServices : ISupportServices
    {
        private readonly IRepository<Support> _repository;

        public SupportServices(IRepository<Support> repository)
        {
            _repository = repository;
        }

        public async Task CreateSupportAsync(CreateSupportDto dto)
        {
            var value = new Support
            {
                Email = dto.Email,
                Name = dto.Name,
                Message = dto.Message,
                Subject = dto.Subject,
                CreatedDate = dto.CreatedDate,
                Status = dto.Status,
            };

            await _repository.CreateAsync(value);
        }

        public async Task DeleteSupportAsync(int id)
        {
            var value = await _repository.GetByIdAsync(id);

            await _repository.DeleteAsync(value);
        }

        public async Task<List<ResultSupportDto>> GetAllSupportAsync()
        {
            var value = await _repository.GetAllAsync();

            return value.Select(x => new ResultSupportDto
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Subject = x.Subject,
                Message = x.Message,
                CreatedDate = x.CreatedDate,
                Status = x.Status,
            }).ToList();
        }

        public async Task<GetByIdSupportDto> GetByIdSupportAsync(int id)
        {
            var value = await _repository.GetByIdAsync(id);

            return new GetByIdSupportDto
            {
                Id = value.Id,
                Name = value.Name,
                Subject = value.Subject,
                Email = value.Email,
                Message = value.Message,
                CreatedDate = value.CreatedDate,
                Status = value.Status,
            };
        }

        public async Task UpdateSupportAsync(UpdateSupportDto dto)
        {
            var value = await _repository.GetByIdAsync(dto.Id);

            value.Name = dto.Name;
            value.Email = dto.Email;
            value.Message = dto.Message;
            value.Subject = dto.Subject;
            value.Status = dto.Status;
            value.CreatedDate = dto.CreatedDate;

            await _repository.UpdateAsync(value);
        }
    }
}
