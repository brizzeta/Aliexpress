using Application.DTOs.Category;
using Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    internal interface ICategoryService
    {
        Task<ApiResponseDto<IEnumerable<CategoryDto>>> GetAllCategoriesAsync();
        Task<ApiResponseDto<CategoryDto>> GetCategoryByIdAsync(int id);
        Task<ApiResponseDto<CategoryDto>> CreateCategoryAsync(CategoryCreateDto dto);
        Task<ApiResponseDto<CategoryDto>> UpdateCategoryAsync(int id, CategoryUpdateDto dto);
        Task<ApiResponseDto<bool>> DeleteCategoryAsync(int id);
        Task<ApiResponseDto<IEnumerable<CategoryDto>>> GetChildCategoriesAsync(int parentId);
    }
}
