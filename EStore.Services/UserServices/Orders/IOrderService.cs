using EStore.Services.SharedResponces;
using EStore.Services.UserServices.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserServices.Orders
{
    public interface IOrderService
    {
        Task<ServiceResponce> MakeOrder(int CartId,string ShippingAddress);
        Task<ServiceResponce> GetOrders(string UserId);
    }
}
