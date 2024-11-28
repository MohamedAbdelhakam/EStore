using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Core.Models
{
    public class ApplicationRole:IdentityRole
    {
        public ApplicationRole(string rolename):base(rolename) 
        {
            
        }
        public ApplicationRole():base()
        {
            
        }
    }
}
