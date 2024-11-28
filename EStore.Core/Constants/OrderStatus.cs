using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Core.Constants
{
    public enum OrderStatus
    {
        Pending,       // 0
        Processing,    // 1
        Shipped,       // 2
        Delivered,     // 3
        Canceled,      // 4
        Returned,      // 5
        Failed,        // 8
        Completed      // 9
    }
}
