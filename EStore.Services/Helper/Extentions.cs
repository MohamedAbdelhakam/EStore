using EStore.Services.SharedResponces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Services.Helper
{
    public static class Extentions
    {
        public static ServiceResponce ExeptionResponce(this object Object, string ExeptionMassege)
        {
            return new ServiceResponce
            {
                Messege = ExeptionMassege,
                IsSucceed = false
            };
        }
    }
}
