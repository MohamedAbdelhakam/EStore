using EStore.Core.Constants;
using EStore.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Services.UserServices.Dtos
{
    public class OrderDto
    {
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public DateTime OrderDate { get; set; } 
        public string ShippingAddress { get; set; }
        public decimal TotalPrice { get; set; }
        public static implicit operator OrderDto(Order order) 
        {
            var orderDto = new OrderDto()
            {
                OrderStatus = order.OrderStatus,
                OrderDate = order.OrderDate,
                ShippingAddress = order.ShippingAddress,
                TotalPrice = order.TotalPrice,

            };
            return orderDto;
        }
    }
}