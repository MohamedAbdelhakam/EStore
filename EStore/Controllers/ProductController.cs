using EStore.Core.Models;
using EStore.Services.AdminServices.Dtos;
using EStore.Services.AdminServices.RepositoryServices.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productServices;

        public ProductController(IProductServices productServices)
        {
            this._productServices = productServices;
        }
        [HttpPost("Add")]
        [Authorize("Administrator")]

        public async Task<IActionResult> AddProduct([FromForm]ProductDto productDto) 
        {
            if(!ModelState.IsValid||productDto is null)
                return BadRequest(ModelState);
            var result=await _productServices.AddProductAsync(productDto);
            if(!result.IsSucceed)
                return BadRequest(result);
            return Ok(result);
        }
        [HttpDelete("delete")]
        [Authorize("Administrator")]
        public async Task<IActionResult> DeleteProduct(int productId) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _productServices.DeleteProductAsync(productId);
            if (!result.IsSucceed)
                return BadRequest(result);
            return Ok(result);
        }
        [HttpDelete("delete/ofCategory")]
        [Authorize("Administrator")]
        public async Task<IActionResult> DeleteProductsInCategory(int categoryId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _productServices.DeleteProductsInCategoryAsync(categoryId);
            if (!result.IsSucceed)
                return BadRequest(result);
            return Ok(result);
        }
        [HttpGet("getproduct")]
        [Authorize("Administrator")]
        [Authorize("Customer")]
        public async Task<IActionResult> GetProduct(int productId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result=await _productServices.GetProductAsync(productId);
            if (!result.IsSucceed)
                return BadRequest(result);
            return Ok(result);
        }
        [HttpGet("getall")]
        [Authorize("Administrator")]
        [Authorize("Customer")]
        public async Task<IActionResult> GetAllProducts()
        {
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var result = await _productServices.GetAllProductsAsync();
                return result.IsSucceed ? Ok(result) : BadRequest(result);
            }
        }
        [HttpGet("Alloutofstock")]
        [Authorize("Administrator")]
        [Authorize("Customer")]
        public async Task<IActionResult> GetProductsOutOfStock()
        {
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var result = await _productServices.GetProductsOutOfStockAsync();
                return result.IsSucceed ? Ok(result) : BadRequest(result);
            }
        }
        [HttpGet("AllInstock")]
        [Authorize("Administrator")]
        [Authorize("Customer")]
        public async Task<IActionResult> GetProductsInStock()
        {
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var result = await _productServices.GetProductsInStockAsync();
                return result.IsSucceed ? Ok(result) : BadRequest(result);
            }
        }
        [HttpGet("InCategory")]
        [Authorize("Administrator")]
        [Authorize("Customer")]
        public async Task<IActionResult> GetProductsOfCategory(int categoryId)
        {
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var result = await _productServices.GetProductsOfCategoryAsync(categoryId);
                return result.IsSucceed ? Ok(result) : BadRequest(result);
            }
        }
    }
}
