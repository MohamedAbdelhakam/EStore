using EStore.Services.AdminServices.Dtos;
using EStore.Services.SharedResponces;

namespace EStore.Services.AdminServices.RepositoryServices.Products
{
    public interface IProductServices
    {
        Task<ServiceResponce> AddProductAsync(ProductDto productDto);
        Task<ServiceResponce> DeleteProductAsync(int productId);
        Task<ServiceResponce> DeleteProductsInCategoryAsync(int CategoryId);
        Task<ServiceResponce> GetProductAsync(int productId);
        Task<ServiceResponce> GetAllProductsAsync();
        Task<ServiceResponce> GetProductsOutOfStockAsync();
        Task<ServiceResponce> GetProductsInStockAsync();
        Task<ServiceResponce> GetProductsOfCategoryAsync(int CategoryId);
    }
}
