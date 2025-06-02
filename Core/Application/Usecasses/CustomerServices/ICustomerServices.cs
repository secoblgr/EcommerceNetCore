using Application.Dtos.CategoryDtos;
using Application.Dtos.CustomerDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Usecasses.CustomerServices
{
    public interface ICustomerServices
    {
        Task<List<ResultCustomerDto>> GetAllCustomerAsync();                          
        Task<GetByIdCustomerDto> GetByIdCustomerAsync(int id);                    
        Task CreateCustomerAsync(CreateCustomerDto model);          
        Task UpdateCustomerAsync(UpdateCustomerDto model);        
        Task DeleteCustomerAsync(int id);
    }
}
