using EStore.Core.AppContexts;
using EStore.Core.Models;
using EStore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EStore.Repositories.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _appDbContext;

        public CartRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }
        public async Task<decimal> AddToCartAsync(int CartId, int ProductId)
        {
            var Product = _appDbContext.Products.FirstOrDefault(p=>p.Id==ProductId);
            if (Product is null)
                throw new Exception($"Product With Id [{ProductId}] Does not Exist");
            var AddedBeforeCartProduct = _appDbContext.CartProducts.FirstOrDefault(cp => cp.CartId==CartId&& cp.ProductId == ProductId);
            if (AddedBeforeCartProduct is null)
            {
                //Never Added
                var CartProduct = new CartProduct
                {
                    CartId = CartId,
                    ProductId = ProductId,
                    Quantity = 1,
                    Price = Product.UnitPrice

                };
                _appDbContext.CartProducts.Add(CartProduct);
            }
            else 
            {
                AddedBeforeCartProduct.Quantity += 1;
                AddedBeforeCartProduct.Price=Product.UnitPrice;
                _appDbContext.CartProducts.Update(AddedBeforeCartProduct);
            }
           return _appDbContext.Cart.First(cp => cp.Id == CartId).TotalPrice += Product.UnitPrice;
        }
        public async Task<decimal> AddQuantityToProductInCartAsync(int CartId, int ProductId, int Quantity)
        {
            var AddedBeforeCartProduct = _appDbContext.CartProducts.FirstOrDefault(cp => cp.CartId == CartId && cp.ProductId == ProductId);
            if (AddedBeforeCartProduct is null)
                throw new Exception("This Product Does Not Exist In Cart");
            Product product = _appDbContext.Products.FirstOrDefault(cp => cp.Id == ProductId);
            if (product is null)
                throw new Exception("Product No Longer Exist ");
            if (product.UnitsInStock < Quantity)
                throw new Exception("This Amount Does Not Available for Now ");

            AddedBeforeCartProduct.Quantity += Quantity;
            AddedBeforeCartProduct.Price += product.UnitPrice*Quantity; 
           _appDbContext.CartProducts.Update(AddedBeforeCartProduct);
            return _appDbContext.Cart.First(cp => cp.Id == CartId).TotalPrice += AddedBeforeCartProduct.Price;
        }


        public async Task<bool> ClearCartAsync(int CartId)
        {
            var CartProductsToRemoveFromCart = _appDbContext.CartProducts.Where(cp=>cp.CartId==CartId);
            _appDbContext.CartProducts.RemoveRange(CartProductsToRemoveFromCart);
            return true;
        }
        public async Task<List<CartProduct>> GetAllProductsInCartAsync(int CartId)
        {
            var AllProductsInCart = _appDbContext.CartProducts.Include(cp=>cp.Product)
                .Where(cp => cp.CartId == CartId)
                .ToList();
            return AllProductsInCart;
        }
        public async Task<decimal> RemoveFromCartAsync(int CartId, int ProductId)
        {
            var AddedBeforeCartProduct = _appDbContext.CartProducts.FirstOrDefault(cp => cp.CartId == CartId && cp.ProductId == ProductId);
            if (AddedBeforeCartProduct is null)
                throw new Exception("This Product Does Not Exist In Cart");

            _appDbContext.CartProducts.Remove(AddedBeforeCartProduct);

            return _appDbContext.Cart.First(cp => cp.Id == CartId).TotalPrice-=AddedBeforeCartProduct.Price;
        }

        public async Task<decimal> RemoveQuantityToProductInCartAsync(int CartId, int ProductId, int Quantity)
        {
            var AddedBeforeCartProduct = _appDbContext.CartProducts.FirstOrDefault(cp => cp.CartId == CartId && cp.ProductId == ProductId);
            if (AddedBeforeCartProduct is null)
                throw new Exception("This Product Does Not Exist In Cart");

            Product product = _appDbContext.Products.FirstOrDefault(cp => cp.Id == ProductId);
            AddedBeforeCartProduct.Quantity -= Quantity;
            AddedBeforeCartProduct.Price -= product.UnitPrice*Quantity;
            _appDbContext.CartProducts.Update(AddedBeforeCartProduct);
            return _appDbContext.Cart.First(cp => cp.Id == CartId).TotalPrice -= AddedBeforeCartProduct.Price;
        }
        public async Task<decimal> GetTotalPriceOfCartAsync(int CartId) 
        {

            var cart = _appDbContext.Cart.First(c => c.Id==CartId);
            return cart.TotalPrice;
        }
    }
}
