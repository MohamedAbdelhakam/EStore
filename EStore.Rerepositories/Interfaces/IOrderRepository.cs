using EStore.Core.Constants;
using EStore.Core.Models;

namespace EStore.Repositories.Interfaces
{
    public interface IOrderRepository 
    {
        Task<bool> OrderAsync(int CartId,string ShippingAddress);   
        Task<ICollection<Order>> GetUserOrdersAsync(string UserId);
        Task<bool> ChangeOrderStatusAsync(int OrderId, OrderStatus orderStatus);
        
    }
}