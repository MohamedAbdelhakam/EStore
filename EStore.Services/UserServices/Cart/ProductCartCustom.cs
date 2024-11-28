using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserServices.Cart
{
    public class ProductCartCustom
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public bool InStock { get; set; }
        public string CoverImageUrl { get; set; }
        public List<string> ImagesUrl { get; set; }
        public int Quantity { get; set; }
        public decimal TotalProductPriceForQuanitity { get; set; }
    }
}
