using EStore.Core.AppContexts;
using EStore.Core.Constants;
using EStore.Core.Models;
using EStore.Repositories.Interfaces;
using System.Collections.Frozen;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations.Schema;

namespace EStore.Repositories.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext appDbContext;

        public OrderRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<bool> ChangeOrderStatusAsync(int OrderId, OrderStatus orderStatus)
        {
            var result = appDbContext.Orders.First(o=>o.Id==OrderId).OrderStatus=orderStatus;
            return true;
        }

        public  async Task<ICollection<Order>> GetUserOrdersAsync(string UserId)
        {
            if (UserId == null) 
                throw new ArgumentNullException(nameof(UserId));
            var orders = appDbContext.Orders
                .Where(o => o.UserId == UserId)
                .ToList().AsReadOnly();
            return orders;
        }

        public async Task<bool> OrderAsync(int CartId,string ShippingAddress)
        {
            var CartProducts=appDbContext.CartProducts.Where(o=>o.CartId==CartId);
            var Cart = appDbContext.Cart.First(C => C.Id == CartId);
            var orderproducts = CartProducts.Select(o => new OrderProduct
            {
                ProductId = o.ProductId,
                Quantity = o.Quantity
            }
            );
            var Order = new Order
            {
                ShippingAddress = ShippingAddress,
                TotalPrice = Cart.TotalPrice,
                OrderProducts = orderproducts.ToList()
            };
            appDbContext.Orders.Add(Order);
            return true;
        }
    }
}