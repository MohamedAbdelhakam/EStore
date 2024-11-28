using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Core.Models
{
    public class ApplicationUser:IdentityUser
    {
        [MaxLength(30)]
        public string FirstName { get; set; }
        [MaxLength(30)]
        public string LastName { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        [MaxLength(100)]
        public string Address { get; set; }
        [ForeignKey("Cart")]
        public int  CartId { get; set; }

        public Cart Cart { get; set; }
        
        public List<RefreshToken> RefreshTokens { get; set; }
    }
}
