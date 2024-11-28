using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Services.SharedDtos
{
    public class RegisterDto
    {
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(50)]
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]

        public string ConfirmedPassword { get; set; }
        
        public string Address { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public string Role { get; set; }

        /*
            Each User Has Admin Id That Company Give Him In Email Or Diffrent Way 
            To Confirm That He is Is verfied By Company 
            =>He Can Not Register With Out It
         */
        public string? AdminId { get; set; }
    }
}