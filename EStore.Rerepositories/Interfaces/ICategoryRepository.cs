using EStore.Core.Models;

namespace EStore.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<bool> AddCatrgoryAsync(Category category);
        Task<bool> UpdateCategoryAsync(Category category);
        Task<IReadOnlyList<Category>> GetAllAsync(int skip,int take);
        Task<Category> GetCategoryByIdAsync(int id);
        Task<bool> DeleteCategoryAsync(int id);
    }
}