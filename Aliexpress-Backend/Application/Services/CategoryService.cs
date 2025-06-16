using Application.DTOs.Category;
using Application.DTOs.Common;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork uof;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork uof, IMapper mapper)
        {
            this.uof = uof;
            this._mapper = mapper;
        }

        public async Task<ApiResponseDto<IEnumerable<CategoryDto>>> GetAllCategoriesAsync()
        {
            var categories = await uof.Categories.GetAllAsync();
            return ApiResponseDto<IEnumerable<CategoryDto>>.SuccessResult(_mapper.Map<IEnumerable<CategoryDto>>(categories));
        }

        public async Task<ApiResponseDto<CategoryDto>> GetCategoryByIdAsync(int id)
        {
            var category = await uof.Categories.GetByIdAsync(id);
            if (category == null)
                return ApiResponseDto<CategoryDto>.FailureResult($"Category with ID {id} not found");

            return ApiResponseDto<CategoryDto>.SuccessResult(_mapper.Map<CategoryDto>(category));
        }

        public async Task<ApiResponseDto<CategoryDto>> CreateCategoryAsync(CategoryCreateDto dto)
        {
            var category = _mapper.Map<Category>(dto);

            await uof.Categories.AddAsync(category);
            await uof.CompleteAsync();

            return ApiResponseDto<CategoryDto>.SuccessResult(_mapper.Map<CategoryDto>(category), "Category created");
        }

        public async Task<ApiResponseDto<CategoryDto>> UpdateCategoryAsync(int id, CategoryUpdateDto dto)
        {
            var category = await uof.Categories.GetByIdAsync(id);
            if (category == null)
                return ApiResponseDto<CategoryDto>.FailureResult("Category not found");

            _mapper.Map(dto, category);

            uof.Categories.Update(category);
            await uof.CompleteAsync();

            return ApiResponseDto<CategoryDto>.SuccessResult(_mapper.Map<CategoryDto>(category), "Category updated");
        }

        public async Task<ApiResponseDto<bool>> DeleteCategoryAsync(int id)
        {
            var category = await uof.Categories.GetByIdAsync(id);
            if (category == null)
                return ApiResponseDto<bool>.FailureResult("Category not found");

            uof.Categories.Delete(category);
            await uof.CompleteAsync();

            return ApiResponseDto<bool>.SuccessResult(true, "Category deleted");
        }

        public async Task<ApiResponseDto<IEnumerable<CategoryDto>>> GetChildCategoriesAsync(int parentId)
        {
            var children = await uof.Categories.FindAsync(c => c.ParentCategoryId == parentId);
            return ApiResponseDto<IEnumerable<CategoryDto>>.SuccessResult(_mapper.Map<IEnumerable<CategoryDto>>(children));
        }
    }

}
