using System.ComponentModel.DataAnnotations;

namespace EStore.Core.Models
{
    public class Category:BaseEntity
    {
        [MaxLength(100)]
        public string CategoryName { get; set; }
        [MaxLength(400)]
        public string Description { get; set; }
        public IList<Product> Products { get; set; }
    }
}