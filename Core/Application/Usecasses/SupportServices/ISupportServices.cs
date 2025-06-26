using Application.Dtos.SubscriberDtos;
using Application.Dtos.SupportDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Usecasses.SupportServices
{
   public  interface ISupportServices
    {
        Task<List<ResultSupportDto>> GetAllSupportAsync();
        Task<GetByIdSupportDto> GetByIdSupportAsync(int id);
        Task CreateSupportAsync(CreateSupportDto dto);
        Task UpdateSupportAsync(UpdateSupportDto dto);
        Task DeleteSupportAsync(int id);
    }
}
