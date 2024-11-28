
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EStore.Core.Models
{
    public class DeletedProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [MaxLength(450)]
        public string Description { get; set; }
        public string Tags { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        [DataType(DataType.Url)]
        public string CoverImageUrl { get; set; }
        public List<string>? ImagesUrl { get; set; }
        public bool InStock => UnitsInStock > 0;
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }

        public static implicit operator DeletedProduct(Product product) 
        {
            return new DeletedProduct
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Tags = product.Tags,
                UnitsInStock = product.UnitsInStock,
                CategoryId = product.CategoryId,
                OrderProducts = product.OrderProducts,
                CoverImageUrl = product.CoverImageUrl,
                ImagesUrl =product.ImagesUrl,  
            };
        }
    }
}