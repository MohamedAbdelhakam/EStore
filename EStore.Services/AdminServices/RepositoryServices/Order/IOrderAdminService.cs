using EStore.Core.Constants;
using EStore.Services.SharedResponces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Services.AdminServices.RepositoryServices.Order
{
    public interface IOrderAdminService
    {
        Task<ServiceResponce> ChangeOrderStatus(int OrderId,OrderStatus orderStatus);
    }
}
