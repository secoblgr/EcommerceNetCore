using Application.Dtos.ContactDtos;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Usecasses.ContactServices
{
    public class ContactServices : IContactServices
    {
        private readonly IRepository<Contact> _repository;

        public ContactServices(IRepository<Contact> repository)
        {
            _repository = repository;
        }

        public async Task CreateContactAsync(CreateContactDto dto)
        {
            var value = new Contact
            {
                Name = dto.Name,
                Email = dto.Email,
                Subject = dto.Subject,
                Message = dto.Message,
                Status = dto.Status,
                CreatedDate = dto.CreatedDate,
            };
            await _repository.CreateAsync(value);
        }

        public async Task DeleteContactAsync(int id)
        {
            var value = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(value);
        }

        public async Task<List<ResultContactDto>> GetAllContactAsync()
        {
            var value = await _repository.GetAllAsync();
            return value.Select(x => new ResultContactDto
            {
                Id = x.Id,
                Name =x.Name,
                Subject = x.Subject,
                Email = x.Email,
                Message = x.Message,
                Status = x.Status,
                CreatedDate = x.CreatedDate
            }).ToList();
        }

        public async Task<GetByIdContactDto> GetByIdContacttAsync(int id)
        {
            var value = await _repository.GetByIdAsync(id);

            return new GetByIdContactDto
            {
                Id = value.Id,
                Name = value.Name,
                Subject = value.Subject,
                Email = value.Email,
                Message = value.Message,
                Status = value.Status,
                CreatedDate = value.CreatedDate
            };

        }

        public async Task UpdateContactAsync(UpdateContactDto dto)
        {
            var value = await _repository.GetByIdAsync(dto.Id);

            value.Name = dto.Name;
            value.Email = dto.Email;
            value.Subject = dto.Subject;
            value.Message = dto.Message;
            value.Status = dto.Status;
            value.CreatedDate = dto.CreatedDate;

            await _repository.UpdateAsync(value);
        }
    }
}
