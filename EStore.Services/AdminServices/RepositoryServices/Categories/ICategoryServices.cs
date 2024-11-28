using AdminServices.Dtos;
using EStore.Core.Models;
using EStore.Services.AdminServices.Dtos;
using EStore.Services.SharedResponces;

namespace AdminCategoryServices
{
    public interface ICategoryServices
    {
        Task<ServiceResponce> AddCategoryAsync(CategoryDto categoryDto);
        Task<ServiceResponce> RemoveCategoryAsync(int CategoryId);
        Task<ServiceResponce> UpdateCategoryAsync(CategoryWithIdDto categoryDto);
        Task<ServiceResponce> GetCategoriesAsync(int PageNumber, int PageSize);
        Task<ServiceResponce> GetCategoryById(int CategoryId);
    }
}
