using AdminCategoryServices;
using AdminServices.Dtos;
using EStore.Services.AdminServices.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServices _categoryServices;

        public CategoryController(ICategoryServices categoryServices)
        {
            this._categoryServices = categoryServices;
        }
        [HttpPost("add")]
        [Authorize("Administrator")]
        public async Task<IActionResult> AddCategory(CategoryDto categoryDto) 
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var result=await _categoryServices.AddCategoryAsync(categoryDto);
            return result.IsSucceed ? Ok(result) : BadRequest(result);
        }
        [HttpDelete("remove")]
        [Authorize("Administrator")]
        public async Task<IActionResult> RemoveCategory(int CategoryId) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result= await _categoryServices.RemoveCategoryAsync(CategoryId);
            return result.IsSucceed ? Ok(result) : BadRequest(result);
        }
        [HttpGet("getall")]
        [Authorize("Administrator")]
        [Authorize("Customer")]
        public async Task<IActionResult> GetCategories(int pagenumber,int pagesize) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result=await _categoryServices.GetCategoriesAsync(pagenumber,pagesize);
            return result.IsSucceed ? Ok(result) : BadRequest(result);
        }
        [HttpGet("GetCategory")]
        [Authorize("Administrator")]
        [Authorize("Customer")]
        public async Task<IActionResult> GetCategory(int CategoryId) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result=await _categoryServices.GetCategoryById(CategoryId);
            return result.IsSucceed ? Ok(result) : BadRequest(result);
        }
        [HttpPost("Update")]
        [Authorize("Administrator")]
        public async Task<IActionResult> UpdateCategory(CategoryWithIdDto categoryRequestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result=await _categoryServices.UpdateCategoryAsync(categoryRequestDto);
            return result.IsSucceed ? Ok(result) : BadRequest(result);
        }
    }
}
