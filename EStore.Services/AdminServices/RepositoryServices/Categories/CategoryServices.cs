
using AdminServices.Dtos;
using EStore.Core.Models;
using EStore.Repositories.Interfaces;
using EStore.Services.AdminServices.Dtos;
using EStore.Services.SharedResponces;
using Microsoft.EntityFrameworkCore;
namespace AdminCategoryServices

{
    public class CategoryServices : ICategoryServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryServices(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponce> AddCategoryAsync(CategoryDto categoryDto)
        {
            if (categoryDto == null)
                return new ServiceResponce
                {
                    Messege="In Valid Data Of Category",
                    IsSucceed=false
                };

            var category = (Category)categoryDto;

            try
            {
                var Addeded = await _unitOfWork.CategoryRepository.AddCatrgoryAsync(category);
                _unitOfWork.Complete();
                return new ServiceResponce
                {
                    IsSucceed = Addeded
                };
            }
            catch (DbUpdateException ex) 
            {
                return new ServiceResponce
                {
                    Messege = "Can Not Add Existing Category Name,You Can Update it",
                    IsSucceed = false
                };
            }
            catch (Exception ex)
            {
                return GetExceptionMessage(ex.Message);

            }
        }

        public async Task<ServiceResponce> GetCategoriesAsync(int PageNumber, int PageSize)
        {
            if (PageNumber < 1 || PageSize < 0)
                return new ServiceResponce
                {
                    Messege = "Data Of pageNumber Or page Size Out Of Range",
                    IsSucceed = false,
                };
            const int MaxmumSize = 30;

            var skip=(PageNumber - 1)*PageSize;
            var Take = Math.Min(MaxmumSize, PageSize);
            var categories=await _unitOfWork.CategoryRepository.GetAllAsync(skip,Take);
            List<CategoryWithIdDto> categoriesResponceDto = new List<CategoryWithIdDto>();
            foreach (var item in categories)
            {
                CategoryWithIdDto categoryResponceDto = (CategoryWithIdDto)item;
                categoriesResponceDto.Add(categoryResponceDto);
            }
            return new ServiceResponce
            {
                Messege="Categories",
                IsSucceed=true,
                Values=
                    {
                        ["Categories"]=(IReadOnlyList<CategoryWithIdDto>)categoriesResponceDto,
                    }
            };
        }

        public async Task<ServiceResponce> RemoveCategoryAsync(int CategoryId)
        {
            var category=await _unitOfWork.CategoryRepository.GetCategoryByIdAsync(CategoryId);
            if (category is null)
                return new ServiceResponce
                {
                    Messege = "This Id Does not exist",
                    IsSucceed = false,
                };
            if(category.Products.Count()>0)
                return new ServiceResponce
                {
                    Messege = "Can not Delete category Has Products In",
                    IsSucceed = false
                };
            
            var Deleted=await _unitOfWork.CategoryRepository.DeleteCategoryAsync(CategoryId);

            _unitOfWork.Complete();

            return new ServiceResponce
            {
                Messege="Deleted",
                IsSucceed=true
            };
        }
        public async Task<ServiceResponce> UpdateCategoryAsync(CategoryWithIdDto categoryDto)
        {
            var category = await _unitOfWork.CategoryRepository.GetCategoryByIdAsync(categoryDto.CategoryId);
            if (category is null)
                return new ServiceResponce
                {
                    Messege="This Category Does Not Exist",
                    IsSucceed = false,
                };
            try
            {
                category.CategoryName = categoryDto.Name;
                category.Description = categoryDto.Description;
                var updated=await _unitOfWork.CategoryRepository.UpdateCategoryAsync(category);
                _unitOfWork.Complete();
                return new ServiceResponce
                {
                    IsSucceed=updated
                };
            }
            catch (Exception ex)
            {
                return GetExceptionMessage(ex.Message);

            }
        }

        public async Task<ServiceResponce> GetCategoryById(int CategoryId)
        {
            var category = await _unitOfWork.CategoryRepository.GetCategoryByIdAsync(CategoryId);
            CategoryWithIdDto CategoryResponce =(CategoryWithIdDto)category;
            return new ServiceResponce
            {
                Messege = "Category",
                IsSucceed = category != null,
                Values =
                {
                    ["Category"]=CategoryResponce
                }
            };
        }
        private ServiceResponce GetExceptionMessage(string Message)
        {
            return new ServiceResponce
            {
                Messege = Message,
                IsSucceed = false,
            };
        }
    }
}
