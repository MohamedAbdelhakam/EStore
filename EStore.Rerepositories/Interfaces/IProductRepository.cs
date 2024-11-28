using EStore.Core.Models;


namespace EStore.Repositories.Interfaces
{
    public interface IProductRepository:IBaseRepository<Product>
    {
        Task<IReadOnlyList<Product>> GetProductOfCategoryAsync(int CategoryId);
        Task<bool> AddProductAsync(Product product);
        Task<bool> DeleteProductAsync(int ProductId);
        Task<bool> DeleteProductsInCategoryAsync(int CategoryId);
        Task<bool> UpdateProductAsync(Product product);
        Task<IReadOnlyList<Product>> GetAllProductsAsync();
        Task<IReadOnlyList<Product>> GetProductsInStockAsync();
        Task<IReadOnlyList<Product>> GetProductsOutOfStockAsync();
        public Task<Product> GetProductByIdAsync(int ProductId);
    }
}
