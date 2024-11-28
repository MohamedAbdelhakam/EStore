

using EStore.Services.SharedResponces;
using System.ComponentModel.DataAnnotations;

namespace UserServices.Cart
{
    public interface ICartSercvice
    {
        public Task<ServiceResponce> AddToCartAsync(int CartId, int ProductId);
        //remove all quantity of product
        public Task<ServiceResponce> RemoveFromCartAsync(int CartId, int ProductId);
        public Task<ServiceResponce> AddQuantityToProductInCartAsync(int CartId,int ProductId,int Quantity);
        public Task<ServiceResponce> RemoveQuantityToProductInCartAsync(int CartId, int ProductId,int Quantity);

        public Task<ServiceResponce> GetAllProductsInCartAsync(int CartId);
        public Task<ServiceResponce> ClearCartAsync(int CartId);

    }
}