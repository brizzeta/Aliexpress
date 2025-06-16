using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Common;
using Application.DTOs.Review;

namespace Application.Interfaces
{
    public interface IReviewService
    {
        Task<ApiResponseDto<ReviewDto>> CreateReviewAsync(ReviewCreateDto reviewCreateDto, int buyerId);
        Task<ApiResponseDto<ReviewDto>> GetReviewByIdAsync(int id);
        Task<ApiResponseDto<IEnumerable<ReviewDto>>> GetAllReviewsAsync();
        Task<ApiResponseDto<IEnumerable<ReviewDto>>> GetReviewsByProductIdAsync(int productId);
        Task<ApiResponseDto<IEnumerable<ReviewDto>>> GetReviewsByBuyerIdAsync(int buyerId);
        Task<ApiResponseDto<IEnumerable<ReviewDto>>> GetReviewsBySellerIdAsync(int sellerId);
        Task<ApiResponseDto<ReviewDto>> UpdateReviewAsync(int id, ReviewUpdateDto reviewUpdateDto, int buyerId);
        Task<ApiResponseDto<bool>> DeleteReviewAsync(int id, int buyerId);
        Task<ApiResponseDto<decimal>> GetAverageProductRatingAsync(int productId);
        Task<ApiResponseDto<decimal>> GetAverageSellerRatingAsync(int sellerId);
    }
}
