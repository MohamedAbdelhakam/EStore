using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Core.Models
{
    public class DeletedCategory
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string CategoryName { get; set; }
        [MaxLength(400)]
        public string Description { get; set; }
        public static implicit operator DeletedCategory(Category category) 
        {
            return new DeletedCategory
            {
                Id = category.Id,
                CategoryName = category.CategoryName,
                Description = category.Description,
            };
        }
    }
}
