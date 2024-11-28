using EStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Services.AdminServices.Dtos
{
    public class ProductWithIdDto:ProductDto
    {
        public int ProductId { get; set; }
        public static implicit operator Product(ProductWithIdDto productDto)
        {
            return new Product
            {
                Id = productDto.ProductId,
                Name = productDto.Name,
                Description = productDto.Description,
                Tags = productDto.Tags,
                UnitPrice = productDto.UnitPrice,
                UnitsInStock = productDto.UnitsInStock,
                CategoryId = productDto.CategoryId,
            };
        }
    }
}
