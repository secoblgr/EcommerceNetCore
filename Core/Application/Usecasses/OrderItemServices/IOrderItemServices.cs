using Application.Dtos.CustomerDtos;
using Application.Dtos.OrderItemDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Usecasses.OrderItemServices
{
    public interface IOrderItemServices
    {
        Task<List<ResultOrderItemDto>> GetAllOrderItemAsync();
        Task<GetByIdOrderItemDto> GetByIdOrderItemAsync(int id);
        Task CreateOrderItemAsync(CreateOrderItemDto model);
        Task UpdateOrderItemAsync(UpdateOrderItemDto model);
        Task DeleteOrderItemAsync(int id);
    }
}
