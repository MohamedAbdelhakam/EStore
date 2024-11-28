using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EStore.Core.Models
{
    public class AdminIdentitfier
    {
        [Key]
        public string AdminId { get; set; }
        public int UserId_Admin { get; set; }
        public ApplicationUser Admin { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Taken { get; set; }
        [ForeignKey("Manager")]
        public string ManagerId { get; set; }
        public ApplicationUser Manager { get; set; }
    }
}
