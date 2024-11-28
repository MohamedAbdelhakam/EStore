using EStore.Core.AppContexts;
using EStore.Core.Models;
using EStore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EStore.Repositories.Repositories
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context) 
        {
            this._context = context;
        }

        public async Task<bool> AddCatrgoryAsync(Category category)
        {
            var AddedCategory=_context.Categories.Add(category);
            return AddedCategory!=null;
        }

        public async Task<IReadOnlyList<Category>> GetAllAsync(int skip,int take)
        {
            var categories =  await _context.Categories.
                Skip(skip).Take(take).ToListAsync();
            return categories?.AsReadOnly();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var category = _context.Categories.Include(c=>c.Products).
                FirstOrDefault(c => c.Id == id);
            return category;
        }
        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            var existingCategory = _context.Categories.
                FirstOrDefault(c => c.Id == category.Id);
            if (existingCategory is null)
                throw new Exception($"Product {category.Id} Does not exists");
            var UpdatedCateogry =_context.Categories.Update(category);
            return UpdatedCateogry != null;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var Category = _context.Categories.FirstOrDefault(c => c.Id == id);

            if (Category is null)
                throw new Exception("This Category Does Not Exist");

            _context.DeletedCategories.Add((DeletedCategory)Category);
            _context.Categories.Remove(Category);
            return true;
        }
    }
}