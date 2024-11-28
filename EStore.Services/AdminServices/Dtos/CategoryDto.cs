

using EStore.Core.Models;

namespace AdminServices.Dtos
{
    public class CategoryDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public static implicit operator Category(CategoryDto category)
        {
            return new Category
            {
                CategoryName = category.Name,
                Description = category.Description
            };
        }
    }
}