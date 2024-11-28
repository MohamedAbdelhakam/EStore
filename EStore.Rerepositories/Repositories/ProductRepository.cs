using EStore.Core.AppContexts;
using EStore.Core.Models;
using EStore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EStore.Repositories.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<bool> AddProductAsync(Product product)
        {
            var category=_context.Categories.
                FirstOrDefault(c=>c.Id==product.CategoryId);
            if (category is null)
                throw new Exception("Category Does not Exist");
            var result=_context.Products.Add(product);
            return true;
        }
        public async Task<IReadOnlyList<Product>> GetProductOfCategoryAsync(int CategoryId)
        {
            var category = _context.Categories.Include(c => c.Products).
                FirstOrDefault(C => C.Id == CategoryId);
            if (category is null)
                throw new Exception($"Category With Id {CategoryId} Does Not Exist");
            var Products = category.Products;

            return Products.ToList();
        }
        public async Task<bool> DeleteProductAsync(int ProductId)
        {
            var Product = _context.Products.FirstOrDefault(p=>p.Id==ProductId);
            if (Product is null)
                throw new Exception("This Product Does Not Exist");

            _context.DeletedProducts.Add((DeletedProduct)Product);
            _context.Products.Remove(Product);

            return true;
        }

        public async Task<bool> DeleteProductsInCategoryAsync(int CategoryId)
        {
            var category = _context.Categories.Include(c => c.Products).
                FirstOrDefault(C => C.Id == CategoryId);

            if (category is null)
                throw new Exception($"Category with Id {CategoryId} Does Not Exist");
            foreach (var item in category.Products) 
            {
                _context.DeletedProducts.Add((DeletedProduct)item);
                _context.Products.Remove(item);
            }
            return true;
        }
        public async Task<bool> UpdateProductAsync(Product product)
        {
            if (product is null)
                throw new Exception($"Product {product.Id} Does not exists");
            var UpdatedProduct=_context.Products.Update(product);
            return UpdatedProduct is not null;
        }

        public async Task<IReadOnlyList<Product>> GetAllProductsAsync()
        {
            var Products = _context.Products.ToList();
            return Products;
        }

        public async Task<IReadOnlyList<Product>> GetProductsInStockAsync()
        {
            var Products = _context.Products.Where(_ => _.InStock).ToList();
            return Products;
        }

        public async Task<IReadOnlyList<Product>> GetProductsOutOfStockAsync()
        {
            var Products = _context.Products.Where(_ => !_.InStock).ToList();
            return Products;
        }

        public async Task<Product> GetProductByIdAsync(int ProductId)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == ProductId);
            if (product is null)
                throw new Exception($"product With Id {ProductId} Does Not Exist");
            return product;
        }
    }
}