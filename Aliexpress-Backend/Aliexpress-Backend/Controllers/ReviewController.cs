using Application.DTOs.Common;
using Application.DTOs.Review;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aliexpress_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        /// <summary>
        /// Получить все отзывы
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<ReviewDto>>>> GetAllReviews()
        {
            var response = await _reviewService.GetAllReviewsAsync();
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        /// <summary>
        /// Получить отзыв по ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponseDto<ReviewDto>>> GetReviewById(int id)
        {
            var response = await _reviewService.GetReviewByIdAsync(id);
            if (!response.Success)
            {
                if (response.Message.Contains("not found"))
                    return NotFound(response);

                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Создать новый отзыв
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponseDto<ReviewDto>>> CreateReview(ReviewCreateDto reviewCreateDto)
        {
            // В реальном приложении BuyerId должен быть получен из токена авторизации
            int buyerId = 1; // Временно для тестирования, позже будет из токена

            var response = await _reviewService.CreateReviewAsync(reviewCreateDto, buyerId);
            if (!response.Success)
                return BadRequest(response);

            return CreatedAtAction(nameof(GetReviewById), new { id = response.Data.Id }, response);
        }

        /// <summary>
        /// Обновить отзыв
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponseDto<ReviewDto>>> UpdateReview(int id, ReviewUpdateDto reviewUpdateDto)
        {
            // В реальном приложении BuyerId должен быть получен из токена авторизации
            int buyerId = 1; // Временно для тестирования, позже будет из токена

            var response = await _reviewService.UpdateReviewAsync(id, reviewUpdateDto, buyerId);
            if (!response.Success)
            {
                if (response.Message.Contains("not found"))
                    return NotFound(response);

                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Удалить отзыв
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponseDto<bool>>> DeleteReview(int id)
        {
            // В реальном приложении BuyerId должен быть получен из токена авторизации
            int buyerId = 1; // Временно для тестирования, позже будет из токена

            var response = await _reviewService.DeleteReviewAsync(id, buyerId);
            if (!response.Success)
            {
                if (response.Message.Contains("not found"))
                    return NotFound(response);

                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Получить отзывы по продукту
        /// </summary>
        [HttpGet("product/{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<ReviewDto>>>> GetReviewsByProduct(int productId)
        {
            var response = await _reviewService.GetReviewsByProductIdAsync(productId);
            if (!response.Success)
            {
                if (response.Message.Contains("not found"))
                    return NotFound(response);

                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Получить отзывы по покупателю
        /// </summary>
        [HttpGet("buyer/{buyerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<ReviewDto>>>> GetReviewsByBuyer(int buyerId)
        {
            var response = await _reviewService.GetReviewsByBuyerIdAsync(buyerId);
            if (!response.Success)
            {
                if (response.Message.Contains("not found"))
                    return NotFound(response);

                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Получить отзывы по продавцу
        /// </summary>
        [HttpGet("seller/{sellerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<ReviewDto>>>> GetReviewsBySeller(int sellerId)
        {
            var response = await _reviewService.GetReviewsBySellerIdAsync(sellerId);
            if (!response.Success)
            {
                if (response.Message.Contains("not found"))
                    return NotFound(response);

                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Получить средний рейтинг продукта
        /// </summary>
        [HttpGet("product/{productId}/rating")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponseDto<decimal>>> GetAverageProductRating(int productId)
        {
            var response = await _reviewService.GetAverageProductRatingAsync(productId);
            if (!response.Success)
            {
                if (response.Message.Contains("not found"))
                    return NotFound(response);

                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Получить средний рейтинг продавца
        /// </summary>
        [HttpGet("seller/{sellerId}/rating")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponseDto<decimal>>> GetAverageSellerRating(int sellerId)
        {
            var response = await _reviewService.GetAverageSellerRatingAsync(sellerId);
            if (!response.Success)
            {
                if (response.Message.Contains("not found"))
                    return NotFound(response);

                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
