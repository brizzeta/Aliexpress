using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Common;
using Application.DTOs.Review;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly IUserService _userService;


        public ReviewService(IUnitOfWork uow, IMapper mapper, IProductService productService, IUserService userService)
        {
            this._uow = uow;
            this._mapper = mapper;
            this._productService = productService;
            this._userService = userService;
        }
        public async Task<ApiResponseDto<ReviewDto>> CreateReviewAsync(ReviewCreateDto reviewCreateDto, int buyerId)
        {
            try
            {
                var product = await _uow.Products.GetByIdAsync(reviewCreateDto.ProductID);
                if (product == null)
                    return ApiResponseDto<ReviewDto>.FailureResult($"Product with ID {reviewCreateDto.ProductID} not found");

                var buyer = await _uow.Users.GetByIdAsync(buyerId);
                if (buyer == null)
                    return ApiResponseDto<ReviewDto>.FailureResult($"Buyer with ID {buyerId} not found");

                var sellerId = product.SellerId;
                var seller = await _uow.Users.GetByIdAsync(sellerId);
                if (seller == null)
                    return ApiResponseDto<ReviewDto>.FailureResult($"Seller with ID {sellerId} not found");

                var existingReview = (await _uow.Reviews.FindAsync(r =>
                    r.BuyerID == buyerId && r.ProductID == reviewCreateDto.ProductID)).FirstOrDefault();

                if (existingReview != null)
                    return ApiResponseDto<ReviewDto>.FailureResult("You have already reviewed this product");

                var hasOrder = (await _uow.Orders.FindAsync(o =>
                    o.BuyerId == buyerId && o.ProductId == reviewCreateDto.ProductID)).Any();

                if (!hasOrder)
                    return ApiResponseDto<ReviewDto>.FailureResult("You can only review products you have purchased");

                var review = _mapper.Map<Review>(reviewCreateDto);
                review.BuyerID = buyerId;
                review.SellerID = sellerId;
                review.CreatedDate = DateTime.UtcNow;

                await _uow.Reviews.AddAsync(review);
                await _uow.CompleteAsync();

                await _productService.UpdateProductRatingAsync(product.Id);

                await _userService.UpdateSellerRatingAsync(sellerId);

                var reviewDto = _mapper.Map<ReviewDto>(review);
                return ApiResponseDto<ReviewDto>.SuccessResult(reviewDto, "Review created successfully");
            }
            catch (Exception ex)
            {
                return ApiResponseDto<ReviewDto>.FailureResult("Error creating review", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<ReviewDto>> GetReviewByIdAsync(int id)
        {
            try
            {
                var review = await _uow.Reviews.GetByIdAsync(id);
                if (review == null)
                    return ApiResponseDto<ReviewDto>.FailureResult($"Review with ID {id} not found");

                var reviewDto = _mapper.Map<ReviewDto>(review);
                return ApiResponseDto<ReviewDto>.SuccessResult(reviewDto);
            }
            catch (Exception ex)
            {
                return ApiResponseDto<ReviewDto>.FailureResult("Error retrieving review", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<IEnumerable<ReviewDto>>> GetAllReviewsAsync()
        {
            try
            {
                var reviews = await _uow.Reviews.GetAllAsync();
                var reviewDtos = _mapper.Map<IEnumerable<ReviewDto>>(reviews);
                return ApiResponseDto<IEnumerable<ReviewDto>>.SuccessResult(reviewDtos);
            }
            catch (Exception ex)
            {
                return ApiResponseDto<IEnumerable<ReviewDto>>.FailureResult("Error retrieving reviews", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<IEnumerable<ReviewDto>>> GetReviewsByProductIdAsync(int productId)
        {
            try
            {
                var product = await _uow.Products.GetByIdAsync(productId);
                if (product == null)
                    return ApiResponseDto<IEnumerable<ReviewDto>>.FailureResult($"Product with ID {productId} not found");

                var reviews = await _uow.Reviews.FindAsync(r => r.ProductID == productId);
                var reviewDtos = _mapper.Map<IEnumerable<ReviewDto>>(reviews);
                return ApiResponseDto<IEnumerable<ReviewDto>>.SuccessResult(reviewDtos);
            }
            catch (Exception ex)
            {
                return ApiResponseDto<IEnumerable<ReviewDto>>.FailureResult("Error retrieving product reviews", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<IEnumerable<ReviewDto>>> GetReviewsByBuyerIdAsync(int buyerId)
        {
            try
            {
                var buyer = await _uow.Users.GetByIdAsync(buyerId);
                if (buyer == null)
                    return ApiResponseDto<IEnumerable<ReviewDto>>.FailureResult($"Buyer with ID {buyerId} not found");

                var reviews = await _uow.Reviews.FindAsync(r => r.BuyerID == buyerId);
                var reviewDtos = _mapper.Map<IEnumerable<ReviewDto>>(reviews);
                return ApiResponseDto<IEnumerable<ReviewDto>>.SuccessResult(reviewDtos);
            }
            catch (Exception ex)
            {
                return ApiResponseDto<IEnumerable<ReviewDto>>.FailureResult("Error retrieving buyer reviews", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<IEnumerable<ReviewDto>>> GetReviewsBySellerIdAsync(int sellerId)
        {
            try
            {
                var seller = await _uow.Users.GetByIdAsync(sellerId);
                if (seller == null)
                    return ApiResponseDto<IEnumerable<ReviewDto>>.FailureResult($"Seller with ID {sellerId} not found");

                var reviews = await _uow.Reviews.FindAsync(r => r.SellerID == sellerId);
                var reviewDtos = _mapper.Map<IEnumerable<ReviewDto>>(reviews);
                return ApiResponseDto<IEnumerable<ReviewDto>>.SuccessResult(reviewDtos);
            }
            catch (Exception ex)
            {
                return ApiResponseDto<IEnumerable<ReviewDto>>.FailureResult("Error retrieving seller reviews", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<ReviewDto>> UpdateReviewAsync(int id, ReviewUpdateDto reviewUpdateDto, int buyerId)
        {
            try
            {
                var review = await _uow.Reviews.GetByIdAsync(id);
                if (review == null)
                    return ApiResponseDto<ReviewDto>.FailureResult($"Review with ID {id} not found");

                if (review.BuyerID != buyerId)
                    return ApiResponseDto<ReviewDto>.FailureResult("You can only update your own reviews");

                if (reviewUpdateDto.Rating.HasValue)
                    review.Rating = reviewUpdateDto.Rating.Value;
                if (reviewUpdateDto.Comment != null)
                    review.Comment = reviewUpdateDto.Comment;

                _uow.Reviews.Update(review);
                await _uow.CompleteAsync();

                await _productService.UpdateProductRatingAsync(review.ProductID);

                await _userService.UpdateSellerRatingAsync(review.SellerID);

                var reviewDto = _mapper.Map<ReviewDto>(review);
                return ApiResponseDto<ReviewDto>.SuccessResult(reviewDto, "Review updated successfully");
            }
            catch (Exception ex)
            {
                return ApiResponseDto<ReviewDto>.FailureResult("Error updating review", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<bool>> DeleteReviewAsync(int id, int buyerId)
        {
            try
            {
                var review = await _uow.Reviews.GetByIdAsync(id);
                if (review == null)
                    return ApiResponseDto<bool>.FailureResult($"Review with ID {id} not found");

                if (review.BuyerID != buyerId)
                    return ApiResponseDto<bool>.FailureResult("You can only delete your own reviews");

                int productId = review.ProductID;
                int sellerId = review.SellerID;

                _uow.Reviews.Delete(review);
                await _uow.CompleteAsync();

                await _productService.UpdateProductRatingAsync(productId);

                await _userService.UpdateSellerRatingAsync(sellerId);

                return ApiResponseDto<bool>.SuccessResult(true, "Review deleted successfully");
            }
            catch (Exception ex)
            {
                return ApiResponseDto<bool>.FailureResult("Error deleting review", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<decimal>> GetAverageProductRatingAsync(int productId)
        {
            try
            {
                var product = await _uow.Products.GetByIdAsync(productId);
                if (product == null)
                    return ApiResponseDto<decimal>.FailureResult($"Product with ID {productId} not found");

                var reviews = await _uow.Reviews.FindAsync(r => r.ProductID == productId);

                if (!reviews.Any())
                    return ApiResponseDto<decimal>.SuccessResult(0, "No reviews found for this product");

                decimal averageRating = reviews.Average(r => r.Rating);
                return ApiResponseDto<decimal>.SuccessResult(averageRating);
            }
            catch (Exception ex)
            {
                return ApiResponseDto<decimal>.FailureResult("Error calculating average product rating", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<decimal>> GetAverageSellerRatingAsync(int sellerId)
        {
            try
            {
                var seller = await _uow.Users.GetByIdAsync(sellerId);
                if (seller == null)
                    return ApiResponseDto<decimal>.FailureResult($"Seller with ID {sellerId} not found");

                var reviews = await _uow.Reviews.FindAsync(r => r.SellerID == sellerId);

                if (!reviews.Any())
                    return ApiResponseDto<decimal>.SuccessResult(0, "No reviews found for this seller");

                decimal averageRating = reviews.Average(r => r.Rating);
                return ApiResponseDto<decimal>.SuccessResult(averageRating);
            }
            catch (Exception ex)
            {
                return ApiResponseDto<decimal>.FailureResult("Error calculating average seller rating", new List<string> { ex.Message });
            }
        }
    }
}
