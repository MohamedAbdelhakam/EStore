using EStore.Core.Models;
using EStore.Repositories.Interfaces;
using EStore.Services.SharedResponces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserServices.Cart;

namespace EStore.Services.UserServices.Cart
{
    public class CartService : ICartSercvice
    {
        private readonly IUnitOfWork unitOfWork;

        public CartService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<ServiceResponce> AddQuantityToProductInCartAsync(int CartId, int ProductId, int Quantity)
        {
            if (Quantity < 1)
                return new ServiceResponce
                {
                    Messege = "Quantity Can not Be less than 1",
                    IsSucceed = false
                };
            try
            {
                var TotalCartPrice = await unitOfWork.CartRepository.AddQuantityToProductInCartAsync(CartId, ProductId, Quantity);
                return new ServiceResponce
                {
                    Messege = $"{Quantity} Of Product With id {ProductId} Added Successfully",
                    IsSucceed = true,
                    Errors = null,
                    Values =
                        {
                            ["Total Price"] = (decimal)TotalCartPrice
                        }
                };
            }
            catch (Exception ex)
            {
                return ExeptionResponce(ex.Message);
            }
        }

        public async Task<ServiceResponce> AddToCartAsync(int CartId, int ProductId)
        {
            try
            {
                var TotalCartPrice = await unitOfWork.CartRepository.AddToCartAsync(CartId, ProductId);
                return new ServiceResponce
                {
                    Messege = "Product Added Successfully",
                    IsSucceed = true,
                    Values =
                        {
                            ["Total Price"] = (decimal)TotalCartPrice
                        }
                };
            }
            catch (Exception ex)
            {
                return ExeptionResponce(ex.Message);

            };
        }

        public async Task<ServiceResponce> ClearCartAsync(int CartId)
        {
            try
            {
                var Cleared = await unitOfWork.CartRepository.ClearCartAsync(CartId);
                if (Cleared)
                {
                    return new ServiceResponce
                    {
                        Messege = "Cart deleted",
                        IsSucceed = true
                    };
                }
                return new ServiceResponce
                {
                    Messege = "Cart Did not Delete",
                    IsSucceed = false
                };
            }
            catch (Exception ex)
            {
                return ExeptionResponce(ex.Message);
            }
        }

        public async Task<ServiceResponce> GetAllProductsInCartAsync(int CartId)
        {
            try
            {
                var CartProducts = await unitOfWork.CartRepository.GetAllProductsInCartAsync(CartId);
                var ProductCartResponce = new List<ProductCartCustom>();

                var productcartResponce = new List<ProductCartCustom>();
                foreach (var cartProduct in CartProducts)
                {
                    productcartResponce.Add(new ProductCartCustom
                    {
                        ProductName = cartProduct.Product.Name,
                        Description = cartProduct.Product.Description,
                        InStock = cartProduct.Product.InStock,
                        CoverImageUrl = cartProduct.Product.CoverImageUrl,
                        ImagesUrl = cartProduct.Product.ImagesUrl,
                        Quantity = cartProduct.Quantity,
                        TotalProductPriceForQuanitity = cartProduct.Price
                    });
                }
                var TotalCartPrice = await unitOfWork.CartRepository.GetTotalPriceOfCartAsync(CartId);
                var NumberOfProductsInCart = CartProducts.Count();
                return new ServiceResponce
                {
                    Messege = "All Products In The Cart",
                    IsSucceed = true,
                    Values =
                    {
                        ["NumberOfProductsInCart"]=(int)NumberOfProductsInCart,
                        ["TotalPrice"]=(decimal)TotalCartPrice,
                        ["Products In Cart"]=(List<ProductCartCustom>)ProductCartResponce,
                    }
                };
            }
            catch (Exception ex)
            {
                return ExeptionResponce(ex.Message);
            }
        }

        public async Task<ServiceResponce> RemoveFromCartAsync(int CartId, int ProductId)
        {
            try
            {
                var TotalCartPrice = await unitOfWork.CartRepository.RemoveFromCartAsync(CartId, ProductId);
                return new ServiceResponce
                {
                    Messege = $"Product of Id {CartId} Removed from Cart",
                    IsSucceed = true,
                    Values =
                    {
                        ["Total Price Of Cart"]=TotalCartPrice
                    }
                };
            }
            catch (Exception ex)
            {
                return ExeptionResponce(ex.Message);
            }
        }
        public async Task<ServiceResponce> RemoveQuantityToProductInCartAsync(int CartId, int ProductId, int Quantity)
        {
            if (Quantity < 1)
                return new ServiceResponce
                {
                    Messege = "Quantity Can not Be less than 1",
                    IsSucceed = false
                };
            try
            {
                var TotalCartPrice = await unitOfWork.CartRepository.
                    RemoveQuantityToProductInCartAsync(CartId, ProductId, Quantity);
                return new ServiceResponce
                {
                    Messege = $"Product of Id {CartId} Removed from Cart",
                    IsSucceed = true,
                    Values =
                    {
                        ["Total Price Of Cart"]=TotalCartPrice
                    }
                };
            }
            catch (Exception ex)
            {
                return ExeptionResponce(ex.Message);
            }
        }
        private ServiceResponce ExeptionResponce(string ExeptionMassege)
        {
            return new ServiceResponce
            {
                Messege = ExeptionMassege,
                IsSucceed = false
            };
        }
    }
}