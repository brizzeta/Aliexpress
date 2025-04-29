using System.Security.Claims;
using Application.DTOs.Common;
using Application.DTOs.Review;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aliexpress_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        [AllowAnonymous] // Разрешаем доступ без авторизации для просмотра всех отзывов
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
        [AllowAnonymous] // Разрешаем доступ без авторизации для просмотра отзыва
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponseDto<ReviewDto>>> CreateReview(ReviewCreateDto reviewCreateDto)
        {
            // Получаем ID пользователя из токена
            var buyerId = GetCurrentUserId();
            if (buyerId == 0)
                return Unauthorized(ApiResponseDto<ReviewDto>.FailureResult("Unauthorized"));

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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponseDto<ReviewDto>>> UpdateReview(int id, ReviewUpdateDto reviewUpdateDto)
        {
            // Получаем ID пользователя из токена
            var buyerId = GetCurrentUserId();
            if (buyerId == 0)
                return Unauthorized(ApiResponseDto<ReviewDto>.FailureResult("Unauthorized"));

            var response = await _reviewService.UpdateReviewAsync(id, reviewUpdateDto, buyerId);
            if (!response.Success)
            {
                if (response.Message.Contains("not found"))
                    return NotFound(response);

                if (response.Message.Contains("only update your own"))
                    return StatusCode(StatusCodes.Status403Forbidden, response);

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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponseDto<bool>>> DeleteReview(int id)
        {
            // Получаем ID пользователя из токена
            var buyerId = GetCurrentUserId();
            if (buyerId == 0)
                return Unauthorized(ApiResponseDto<bool>.FailureResult("Unauthorized"));

            // Проверка роли администратора
            bool isAdmin = User.IsInRole("Admin");

            // Если это админ, то получим отзыв и сначала проверим его существование
            if (isAdmin)
            {
                var reviewCheckResponse = await _reviewService.GetReviewByIdAsync(id);
                if (!reviewCheckResponse.Success)
                    return NotFound(reviewCheckResponse);

                // Для админа удаляем любой отзыв, независимо от владельца
                // Но так как наш сервис не поддерживает параметр isAdmin,
                // мы используем ID пользователя из отзыва
                var reviewData = reviewCheckResponse.Data;
                var response = await _reviewService.DeleteReviewAsync(id, reviewData.BuyerID);
                return Ok(response);
            }
            else
            {
                // Для обычного пользователя используем стандартную проверку владельца
                var response = await _reviewService.DeleteReviewAsync(id, buyerId);
                if (!response.Success)
                {
                    if (response.Message.Contains("not found"))
                        return NotFound(response);

                    if (response.Message.Contains("can only delete your own"))
                        return StatusCode(StatusCodes.Status403Forbidden, response);

                    return BadRequest(response);
                }
                return Ok(response);
            }
        }

        /// <summary>
        /// Получить отзывы по продукту
        /// </summary>
        [HttpGet("product/{productId}")]
        [AllowAnonymous] // Разрешаем доступ без авторизации
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
        [AllowAnonymous] // Разрешаем доступ без авторизации
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
        /// Получить свои отзывы
        /// </summary>
        [HttpGet("my-reviews")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<ReviewDto>>>> GetMyReviews()
        {
            var buyerId = GetCurrentUserId();
            if (buyerId == 0)
                return Unauthorized(ApiResponseDto<IEnumerable<ReviewDto>>.FailureResult("Unauthorized"));

            var response = await _reviewService.GetReviewsByBuyerIdAsync(buyerId);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        /// <summary>
        /// Получить отзывы по продавцу
        /// </summary>
        [HttpGet("seller/{sellerId}")]
        [AllowAnonymous] // Разрешаем доступ без авторизации
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
        [AllowAnonymous] // Разрешаем доступ без авторизации
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
        [AllowAnonymous] // Разрешаем доступ без авторизации
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

        /// <summary>
        /// Получить ID текущего пользователя из JWT-токена
        /// </summary>
        private int GetCurrentUserId()
        {
            if (User.Identity?.IsAuthenticated != true)
                return 0;

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                return userId;

            return 0;
        }
    }
}
