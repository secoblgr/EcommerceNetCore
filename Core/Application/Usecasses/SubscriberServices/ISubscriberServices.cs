using Application.Dtos.SubscriberDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Usecasses.SubscriberServices
{
    public interface ISubscriberServices
    {
        Task<List<ResultSubscriberDto>> GetAllSubscriberAsync();
        Task<GetByIdSubscriberDto> GetByIdSubscriberAsync(int id);
        Task CreateSubscriberAsync(CreateSubscriberDto dto);
        Task UpdateSubscriberAsync(UpdateSubscriberDto dto);
        Task DeleteSubscriberAsync(int id);
    }
}
