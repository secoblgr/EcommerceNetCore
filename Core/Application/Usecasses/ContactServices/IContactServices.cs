using Application.Dtos.ContactDtos;
using Application.Dtos.SupportDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Usecasses.ContactServices
{
    public interface IContactServices
    {
        Task<List<ResultContactDto>> GetAllContactAsync();
        Task<GetByIdContactDto> GetByIdContacttAsync(int id);
        Task CreateContactAsync(CreateContactDto dto);
        Task UpdateContactAsync(UpdateContactDto dto);
        Task DeleteContactAsync(int id);
    }
}
