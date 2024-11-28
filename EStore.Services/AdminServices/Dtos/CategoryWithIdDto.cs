using AdminServices.Dtos;
using EStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Services.AdminServices.Dtos
{
    public class CategoryWithIdDto:CategoryDto
    {
        public int CategoryId { get; set; }
        public static implicit operator CategoryWithIdDto(Category category) 
        {
            return new CategoryWithIdDto
            {
                CategoryId=category.Id,
                Name = category.CategoryName,
                Description = category.Description
            };
        }
    }
}
