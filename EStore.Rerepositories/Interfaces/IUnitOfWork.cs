using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Repositories.Interfaces
{
    public interface IUnitOfWork 
    {
        ICategoryRepository CategoryRepository { get; }
        IOrderRepository OrderRepository { get; }
        IProductRepository ProductRepository { get; }
        ICartRepository CartRepository { get; }
        IRefreshTokenRepsitory RefreshTokenRepsitory { get; }
        void Complete();
    }
}
