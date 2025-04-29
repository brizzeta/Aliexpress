using Application.DTOs.Category;
using Application.DTOs.Common;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aliexpress_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<CategoryDto>>>> GetAllCategories()
        {
            var response = await _categoryService.GetAllCategoriesAsync();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponseDto<CategoryDto>>> GetCategory(int id)
        {
            var response = await _categoryService.GetCategoryByIdAsync(id);

            if (!response.Success || response.Data == null)
                return NotFound(response);

            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponseDto<CategoryDto>>> CreateCategory([FromBody] CategoryCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _categoryService.CreateCategoryAsync(dto);

            if (!response.Success)
                return BadRequest(response);

            return CreatedAtAction(nameof(GetCategory), new { id = response.Data?.Id }, response);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponseDto<CategoryDto>>> UpdateCategory(int id, [FromBody] CategoryUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _categoryService.UpdateCategoryAsync(id, dto);

            if (!response.Success)
                return response.Message.Contains("not found") ? NotFound(response) : BadRequest(response);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponseDto<bool>>> DeleteCategory(int id)
        {
            var response = await _categoryService.DeleteCategoryAsync(id);

            if (!response.Success)
                return response.Message.Contains("not found") ? NotFound(response) : BadRequest(response);

            return Ok(response);
        }

        [HttpGet("children/{parentId}")]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<CategoryDto>>>> GetChildCategories(int parentId)
        {
            var response = await _categoryService.GetChildCategoriesAsync(parentId);

            if (!response.Success)
                return NotFound(response);

            return Ok(response);
        }
    }
}
