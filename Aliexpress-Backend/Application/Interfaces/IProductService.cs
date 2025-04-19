using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Common;
using Application.DTOs.Product;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IProductService
    {
        Task<ApiResponseDto<ProductDetailDto>> GetProductByIdAsync(int id);
        Task<ApiResponseDto<IEnumerable<ProductDto>>> GetAllProductsAsync();
        Task<ApiResponseDto<ProductDto>> CreateProductAsync(ProductCreateDto productCreateDto);
        Task<ApiResponseDto<ProductDto>> UpdateProductAsync(int id, ProductUpdateDto productUpdateDto);
        Task<ApiResponseDto<bool>> DeleteProductAsync(int id);
        Task<ApiResponseDto<IEnumerable<ProductDto>>> GetProductsByCategoryAsync(int categoryId);
        Task<ApiResponseDto<IEnumerable<ProductDto>>> GetProductsBySellerAsync(int sellerId);
        Task<ApiResponseDto<IEnumerable<ProductDto>>> SearchProductsAsync(string searchTerm);
        Task<ApiResponseDto<bool>> UpdateProductStockAsync(int productId, int newStock);
        Task<ApiResponseDto<bool>> HardDeleteProductAsync(int id);
        Task<ApiResponseDto<IEnumerable<ProductDto>>> GetDiscountedProductsAsync();
        Task<ApiResponseDto<IEnumerable<ProductDto>>> GetTopRatedProductsAsync(int count = 10);
        Task<ApiResponseDto<IEnumerable<ProductDto>>> GetRelatedProductsAsync(int productId, int count = 5);
        Task<ApiResponseDto<bool>> UpdateProductImagesAsync(int productId, List<string> imageUrls);
        Task<ApiResponseDto<bool>> UpdateProductRatingAsync(int productId);
    }
}
