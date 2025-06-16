using Application.Dtos.OrderItemDtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.OrderDtos
{
    public class CreateOrderDto
    {
        public DateTime OrderDate { get; set; }
      //  public decimal TotalAmount { get; set; }
        public string OrderStatus { get; set; }
        public string ShippingAddress { get; set; }
        public int CustomerId { get; set; }
        //  public Customer Customer { get; set; }
        public ICollection<CreateOrderItemDto> OrderItems { get; set; }
    }
}
