using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Core.Models
{
    public class Product : BaseEntity
    {
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
    }
}