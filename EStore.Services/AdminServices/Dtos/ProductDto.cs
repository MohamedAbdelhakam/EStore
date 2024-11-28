using EStore.Core.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace EStore.Services.AdminServices.Dtos
{
    public class ProductDto
    {
        public string Name { get; set; }
        [MaxLength(450)]
        public string Description { get; set; }
        public string Tags { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public IFormFile ?CoverImage { get; set; }
        public List<IFormFile>? Images { get; set; }
        public int CategoryId { get; set; }

        public static implicit operator Product(ProductDto productDto) 
        {
            return new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Tags = productDto.Tags,
                UnitPrice = productDto.UnitPrice,
                UnitsInStock = productDto.UnitsInStock,
                CategoryId = productDto.CategoryId
            };
        }
    }
}