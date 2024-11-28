using System.ComponentModel.DataAnnotations.Schema;

namespace EStore.Core.Models
{
    public class Cart:BaseEntity
    {
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int NumberOfProducts { get; set; }
        public decimal TotalPrice { get; set; }
    }
}