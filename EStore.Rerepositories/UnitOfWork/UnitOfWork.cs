using EStore.Core.AppContexts;
using EStore.Repositories.Interfaces;
using EStore.Repositories.Repositories;

namespace EStore.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;
        public ICategoryRepository CategoryRepository { get; set; }
        public IOrderRepository OrderRepository { get; set; }
        public IProductRepository ProductRepository { get; set; }
        public ICartRepository CartRepository { get; set; }
        public IRefreshTokenRepsitory RefreshTokenRepsitory { get; set; }
        public UnitOfWork(AppDbContext context)
        {
            this._context = context;
            CartRepository=new CartRepository(context);
            CategoryRepository = new CategoryRepository(context);
            OrderRepository = new OrderRepository(context);
            ProductRepository = new ProductRepository(context);
            RefreshTokenRepsitory=new RefreshTokenRepository(context);
        }
        public void Complete()
        {
            _context.SaveChanges();
        }
        void IDisposable.Dispose()
        {
            _context.Dispose();
        }
    }
}
