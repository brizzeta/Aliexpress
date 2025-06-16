using System.Security.Claims;
using Application.DTOs.Common;
using Application.DTOs.Product;
using Application.Interfaces;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aliexpress_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Получить все продукты
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<ProductDto>>>> GetAllProducts()
        {
            var response = await _productService.GetAllProductsAsync();
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        /// <summary>
        /// Получить продукт по ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponseDto<ProductDetailDto>>> GetProductById(int id)
        {
            var response = await _productService.GetProductByIdAsync(id);
            if (!response.Success)
            {
                if (response.Message.Contains("not found"))
                    return NotFound(response);

                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Создать новый продукт
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Seller,Admin,SuperAdmin")]
        public async Task<ActionResult<ApiResponseDto<ProductDto>>> CreateProduct(ProductCreateDto productCreateDto)
        {
            // Добавляем ID продавца из токена, если это продавец
            if (User.IsInRole(UserRole.Seller.ToString()))
            {
                if (int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int sellerId))
                {
                    // Автоматически устанавливаем значение SellerId из токена
                    productCreateDto.SellerId = sellerId;
                }
                else
                {
                    return BadRequest(ApiResponseDto<ProductDto>.FailureResult("Invalid user ID in token"));
                }
            }

            var response = await _productService.CreateProductAsync(productCreateDto);
            if (!response.Success)
                return BadRequest(response);

            return CreatedAtAction(nameof(GetProductById), new { id = response.Data.Id }, response);
        }

        /// <summary>
        /// Обновить продукт
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Seller,Admin,SuperAdmin")]
        public async Task<ActionResult<ApiResponseDto<ProductDto>>> UpdateProduct(int id, ProductUpdateDto productUpdateDto)
        {
            // Проверяем права доступа
            if (!await HasAccessToProduct(id))
                return Forbid();

            var response = await _productService.UpdateProductAsync(id, productUpdateDto);
            if (!response.Success)
            {
                if (response.Message.Contains("not found"))
                    return NotFound(response);

                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Удалить продукт (мягкое удаление)
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Seller,Admin,SuperAdmin")]
        public async Task<ActionResult<ApiResponseDto<bool>>> DeleteProduct(int id)
        {
            // Проверяем права доступа
            if (!await HasAccessToProduct(id))
                return Forbid();

            var response = await _productService.DeleteProductAsync(id);
            if (!response.Success)
            {
                if (response.Message.Contains("not found"))
                    return NotFound(response);

                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Полное удаление продукта из базы данных
        /// </summary>
        [HttpDelete("hard/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<ActionResult<ApiResponseDto<bool>>> HardDeleteProduct(int id)
        {
            var response = await _productService.HardDeleteProductAsync(id);
            if (!response.Success)
            {
                if (response.Message.Contains("not found"))
                    return NotFound(response);

                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Получить продукты по категории
        /// </summary>
        [HttpGet("category/{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<ProductDto>>>> GetProductsByCategory(int categoryId)
        {
            var response = await _productService.GetProductsByCategoryAsync(categoryId);
            if (!response.Success)
            {
                if (response.Message.Contains("not found"))
                    return NotFound(response);

                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Получить продукты продавца
        /// </summary>
        [HttpGet("seller/{sellerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<ProductDto>>>> GetProductsBySeller(int sellerId)
        {
            var response = await _productService.GetProductsBySellerAsync(sellerId);
            if (!response.Success)
            {
                if (response.Message.Contains("not found"))
                    return NotFound(response);

                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Поиск продуктов по ключевому слову
        /// </summary>
        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<ProductDto>>>> SearchProducts([FromQuery] string query)
        {
            var response = await _productService.SearchProductsAsync(query);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        /// <summary>
        /// Получить продукты со скидкой
        /// </summary>
        [HttpGet("discounted")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<ProductDto>>>> GetDiscountedProducts()
        {
            var response = await _productService.GetDiscountedProductsAsync();
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        /// <summary>
        /// Получить наиболее рейтинговые продукты
        /// </summary>
        [HttpGet("top-rated")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<ProductDto>>>> GetTopRatedProducts([FromQuery] int count = 10)
        {
            var response = await _productService.GetTopRatedProductsAsync(count);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        /// <summary>
        /// Получить связанные продукты
        /// </summary>
        [HttpGet("{id}/related")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<ProductDto>>>> GetRelatedProducts(int id, [FromQuery] int count = 5)
        {
            var response = await _productService.GetRelatedProductsAsync(id, count);
            if (!response.Success)
            {
                if (response.Message.Contains("not found"))
                    return NotFound(response);

                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Обновить изображения продукта
        /// </summary>
        [HttpPut("{id}/images")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Seller,Admin,SuperAdmin")]
        public async Task<ActionResult<ApiResponseDto<bool>>> UpdateProductImages(int id, [FromBody] List<string> imageUrls)
        {
            // Проверяем права доступа
            if (!await HasAccessToProduct(id))
                return Forbid();

            var response = await _productService.UpdateProductImagesAsync(id, imageUrls);
            if (!response.Success)
            {
                if (response.Message.Contains("not found"))
                    return NotFound(response);

                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Обновить количество товара на складе
        /// </summary>
        [HttpPut("{id}/stock")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Seller,Admin,SuperAdmin")]
        public async Task<ActionResult<ApiResponseDto<bool>>> UpdateProductStock(int id, [FromBody] int newStock)
        {
            // Проверяем права доступа
            if (!await HasAccessToProduct(id))
                return Forbid();

            var response = await _productService.UpdateProductStockAsync(id, newStock);
            if (!response.Success)
            {
                if (response.Message.Contains("not found"))
                    return NotFound(response);

                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Обновить рейтинг продукта
        /// </summary>
        [HttpPut("{id}/rating")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<ActionResult<ApiResponseDto<bool>>> UpdateProductRating(int id)
        {
            var response = await _productService.UpdateProductRatingAsync(id);
            if (!response.Success)
            {
                if (response.Message.Contains("not found"))
                    return NotFound(response);

                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Проверяет, имеет ли текущий пользователь доступ к продукту с указанным ID
        /// </summary>
        private async Task<bool> HasAccessToProduct(int productId)
        {
            // Получаем ID текущего пользователя из токена
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int currentUserId))
                return false;

            // Проверяем роль пользователя
            bool isAdmin = User.IsInRole(UserRole.Admin.ToString()) || User.IsInRole(UserRole.SuperAdmin.ToString());
            bool isSeller = User.IsInRole(UserRole.Seller.ToString());

            // Администраторы имеют доступ ко всем продуктам
            if (isAdmin)
                return true;

            // Для продавцов проверяем, принадлежит ли продукт им
            if (isSeller)
            {
                // Получаем продукт для проверки
                var productResponse = await _productService.GetProductByIdAsync(productId);

                // Если продукт не найден, возвращаем false
                if (!productResponse.Success || productResponse.Data == null)
                    return false;

                // Проверяем, принадлежит ли продукт текущему продавцу
                return productResponse.Data.SellerId == currentUserId;
            }

            // Для всех остальных ролей (покупатели и т.д.) доступ запрещен
            return false;
        }
    }
}