using EStore.Core.Models;
using EStore.Repositories.Interfaces;
using EStore.Services.AdminServices.Dtos;
using EStore.Services.SharedResponces;
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Asn1.Crmf;

namespace EStore.Services.AdminServices.RepositoryServices.Products
{
    public class ProductServices : IProductServices
    {
        private readonly IConfiguration configuration;
        private readonly IUnitOfWork unitOfWork;

        public ProductServices(IConfiguration configuration,IUnitOfWork unitOfWork)
        {
            this.configuration = configuration;
            this.unitOfWork = unitOfWork;
        }
        public async Task<ServiceResponce> AddProductAsync(ProductDto ProductDto)
        {
            if (ProductDto is null)
                return new ServiceResponce
                {
                    Messege = "Invalid Data",
                    IsSucceed = true,
                };
            var Product = (Product)ProductDto;            
            var ImagefolderPath = Path.Combine(configuration["productImagesPath"], ProductDto.Name);
            if (!Directory.Exists(ImagefolderPath))
            {
                Directory.CreateDirectory(ImagefolderPath);
            }

            var CoverImageName = "CoverOf" + ProductDto.Name.ToLower() + Guid.NewGuid() +
                Path.GetExtension(ProductDto.CoverImage.FileName);
            var CoverImagePath = Path.Combine(ImagefolderPath, CoverImageName);
            using (var stream = new FileStream(CoverImagePath, FileMode.Create))
            {
                await ProductDto.CoverImage.CopyToAsync(stream);
            }
            Product.CoverImageUrl = CoverImagePath;
            

            Product.ImagesUrl = new List<string>();
            if (ProductDto.Images is not null) 
            {
                foreach (var image in ProductDto.Images)
                {
                    var productImageName = ProductDto.Name.ToLower() + Guid.NewGuid() +
                                            Path.GetExtension(image.FileName);

                    var imagePath = Path.Combine(ImagefolderPath, productImageName);

                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }
                    Product.ImagesUrl.Add(imagePath);
                }
            }
            try
            {
                var result = await unitOfWork.ProductRepository.AddProductAsync(Product);
                if (!result)
                    return new ServiceResponce
                    {
                        Messege ="The Product has not been added",
                        IsSucceed = false,
                    };
                unitOfWork.Complete();
                return new ServiceResponce
                {
                    Messege = "Added",
                    IsSucceed = true,
                };
            }
            catch (Exception ex)
            {
                return GetExceptionMessage(ex.Message);

            }

        }
        public async Task<ServiceResponce> DeleteProductAsync(int productId)
        {
            try
            {
                var result = await unitOfWork.ProductRepository.DeleteProductAsync(productId);
                if (!result)
                    return new ServiceResponce
                    {
                        Messege=$"There is No Product with Id {productId}",
                        IsSucceed = false,
                    };
                unitOfWork.Complete();
                return new ServiceResponce
                {
                    Messege = "Deleted Successfully",
                    IsSucceed = true,
                };
            }
            catch (Exception ex) 
            {
                return GetExceptionMessage(ex.Message);

            }
        }

        public async Task<ServiceResponce> DeleteProductsInCategoryAsync(int CategoryId)
        {
            try
            {
                var result = await unitOfWork.ProductRepository.DeleteProductsInCategoryAsync(CategoryId);
                if (!result)
                    return new ServiceResponce
                    {
                        Messege = "Invalid Operation",
                        IsSucceed = false,
                    };
                unitOfWork.Complete();
                return new ServiceResponce
                {
                    Messege = $"Products in Category of id {CategoryId} Deleted",
                    IsSucceed = true,
                };
            }
            catch (Exception ex)
            {
                return GetExceptionMessage(ex.Message);

            }
        }

        public async Task<ServiceResponce> GetAllProductsAsync()
        {
            try
            {
                var Products=await unitOfWork.ProductRepository.GetAllProductsAsync();
                return new ServiceResponce
                {
                    Messege="Products",
                    IsSucceed=true,
                    Values = 
                        {
                            ["Products"]=(IReadOnlyList<Product>)Products
                        }
                };
            }
            catch (Exception ex) 
            {
                return GetExceptionMessage(ex.Message);

            }
        }

        public async Task<ServiceResponce> GetProductAsync(int productId)
        {
            try
            {
                var Product=await unitOfWork.ProductRepository.GetProductByIdAsync(productId);
                return new ServiceResponce
                {
                    Messege = "Product",
                    IsSucceed = true,
                    Values =
                    {
                        ["Product"]=(Product)Product
                    }
                };
            }
            catch (Exception ex)
            {
                return GetExceptionMessage(ex.Message);

            }
        }

        public async Task<ServiceResponce> GetProductsOfCategoryAsync(int CategoryId)
        {
            try
            {
                var ProductsOfCategory=await unitOfWork.ProductRepository.
                    GetProductOfCategoryAsync(CategoryId);
                return new ServiceResponce
                {
                    Messege= $"Products Of Category Id {CategoryId}",
                    IsSucceed = true,
                    Values = 
                    {
                        ["Products"]=(IReadOnlyList<Product>)ProductsOfCategory
                    }
                };
            }
            
            catch (Exception ex)
            {
                return GetExceptionMessage(ex.Message);

            }
        }

        public async Task<ServiceResponce> GetProductsInStockAsync()
        {
            try
            {
                IReadOnlyList<Product> products=await unitOfWork.ProductRepository.GetProductsInStockAsync();
                return new ServiceResponce
                {
                    Messege="Products In Stock",
                    IsSucceed = true,
                    Values = 
                    {
                        ["Products"]=(IReadOnlyList<Product>)products
                    }
                };
            }   

            catch (Exception ex) 
            {
                return GetExceptionMessage(ex.Message);

            }
        }

        public async Task<ServiceResponce> GetProductsOutOfStockAsync()
        {
            try
            {
                var products=await unitOfWork.ProductRepository.GetProductsOutOfStockAsync();
                return new ServiceResponce
                {
                    Messege = "Products Out Of Stock",
                    IsSucceed = true,
                    Values =
                    {
                        ["Products"]=(IReadOnlyList<Product>)products
                    }
                };
            }
            catch (Exception ex) 
            {
                return GetExceptionMessage(ex.Message);

            }
        }
        private ServiceResponce GetExceptionMessage(string Message) 
        {
            return new ServiceResponce
            {
                Messege = Message,
                IsSucceed = false,
            };
        }
    }
}
