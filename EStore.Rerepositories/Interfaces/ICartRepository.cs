using EStore.Core.Models;
using EStore.Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Repositories.Interfaces
{
    public interface ICartRepository
    {
        public Task<decimal> AddToCartAsync(int CartId, int ProductId);
        //remove all quantity of product
        public Task<decimal> RemoveFromCartAsync(int CartId,int ProductId);
        public Task<decimal> AddQuantityToProductInCartAsync(int CartId, int ProductId, int Quantity);
        public Task<decimal> RemoveQuantityToProductInCartAsync(int CartId, int ProductId, int Quantity);

        public Task<List<CartProduct>> GetAllProductsInCartAsync(int CartId);
        public Task<bool> ClearCartAsync(int CartId);
        public Task<decimal> GetTotalPriceOfCartAsync(int CartId);
    }
}