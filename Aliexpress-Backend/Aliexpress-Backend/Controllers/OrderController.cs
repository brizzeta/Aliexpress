using System.Security.Claims;
using Application.DTOs.Common;
using Application.DTOs.Order;
using Application.Interfaces;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aliexpress_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Получить все заказы (только для администраторов)
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<OrderDto>>>> GetAllOrders()
        {
            var response = await _orderService.GetAllOrdersAsync();

            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }

        /// <summary>
        /// Получить заказ по ID
        /// </summary>
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponseDto<OrderDetailDto>>> GetOrderById(int id)
        {
            var response = await _orderService.GetOrderByIdAsync(id);

            if (!response.Success)
                return BadRequest(response);

            if (response.Data == null)
                return NotFound(response);

            string userRole = User.FindFirst(ClaimTypes.Role)?.Value!;
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            bool isAdmin = userRole == UserRole.Admin.ToString() || userRole == UserRole.SuperAdmin.ToString();

            if (!isAdmin)
            {
                bool isBuyer = userRole == UserRole.Buyer.ToString() && response.Data.BuyerId == userId;
                bool isSeller = userRole == UserRole.Seller.ToString()
                    && response.Data.Product?.SellerId == userId;

                if (!isBuyer && !isSeller)
                    return Forbid();
            }

            return Ok(response);
        }

        /// <summary>
        /// Получить заказы текущего покупателя
        /// </summary>
        [HttpGet("my")]
        [Authorize(Roles = "Buyer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<OrderDto>>>> GetMyOrders()
        {
            int buyerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var response = await _orderService.GetOrdersByBuyerIdAsync(buyerId);

            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }

        /// <summary>
        /// Получить заказы по ID покупателя (только для администраторов)
        /// </summary>
        [HttpGet("buyer/{buyerId}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<OrderDto>>>> GetOrdersByBuyerId(int buyerId)
        {
            var response = await _orderService.GetOrdersByBuyerIdAsync(buyerId);

            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }

        /// <summary>
        /// Получить заказы по ID товара
        /// </summary>
        [HttpGet("product/{productId}")]
        [Authorize(Roles = "Seller,Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<OrderDto>>>> GetOrdersForProduct(int productId)
        {
            string userRole = User.FindFirst(ClaimTypes.Role)?.Value!;
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            bool isAdmin = userRole == UserRole.Admin.ToString() || userRole == UserRole.SuperAdmin.ToString();

            // Для продавцов проверяем принадлежность товара путем получения заказов
            // и проверки первого заказа (если он есть)
            if (!isAdmin && userRole == UserRole.Seller.ToString())
            {
                // Получаем ответ с заказами по данному productId
                var tempResponse = await _orderService.GetOrdersForProductAsync(productId);

                // Если нет заказов или произошла ошибка - проверить владение нельзя
                // (здесь имеет смысл вернуть пустой список успешно, так как заказов просто нет)
                if (!tempResponse.Success)
                    return BadRequest(tempResponse);

                // Если есть заказы, проверяем принадлежность товара продавцу
                // по первому из них (все заказы относятся к одному продукту с одним продавцом)
                var orders = tempResponse.Data.ToList();
                if (orders.Any())
                {
                    // Получаем детальную информацию о заказе для проверки sellerId
                    var orderDetail = await _orderService.GetOrderByIdAsync(orders.First().Id);
                    if (orderDetail.Success && orderDetail.Data?.Product != null &&
                        orderDetail.Data.Product.SellerId != userId)
                    {
                        return Forbid();
                    }
                }
                // Если заказов нет, получаем пустой список
            }

            var response = await _orderService.GetOrdersForProductAsync(productId);

            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }

        /// <summary>
        /// Создать новый заказ
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Buyer")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponseDto<OrderDto>>> CreateOrder([FromBody] OrderCreateDto orderCreateDto)
        {
            // Устанавливаем ID покупателя из токена
            int buyerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            orderCreateDto.BuyerId = buyerId;

            var response = await _orderService.CreateOrderAsync(orderCreateDto);

            if (!response.Success)
                return BadRequest(response);

            return CreatedAtAction(nameof(GetOrderById), new { id = response.Data.Id }, response);
        }

        /// <summary>
        /// Обновить заказ
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Buyer,Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponseDto<bool>>> UpdateOrder(int id, [FromBody] OrderUpdateDto orderUpdateDto)
        {
            // Проверяем, что пользователь имеет право обновлять этот заказ
            string userRole = User.FindFirst(ClaimTypes.Role)?.Value!;
            bool isAdmin = userRole == UserRole.Admin.ToString() || userRole == UserRole.SuperAdmin.ToString();

            if (!isAdmin)
            {
                // Проверка, что заказ принадлежит текущему покупателю
                var orderCheck = await _orderService.GetOrderByIdAsync(id);
                if (!orderCheck.Success || orderCheck.Data == null)
                    return NotFound(new ApiResponseDto<bool> { Success = false, Message = "Order not found" });

                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                if (orderCheck.Data.BuyerId != userId)
                    return Forbid();
            }

            var response = await _orderService.UpdateOrderAsync(id, orderUpdateDto);

            if (!response.Success)
            {
                if (response.Message.Contains("not found"))
                    return NotFound(response);

                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Обновить статус заказа
        /// </summary>
        [HttpPatch("{id}/status")]
        [Authorize(Roles = "Seller,Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponseDto<bool>>> UpdateOrderStatus(int id, [FromBody] OrderStatusUpdateDto statusUpdateDto)
        {
            string userRole = User.FindFirst(ClaimTypes.Role)?.Value!;
            bool isAdmin = userRole == UserRole.Admin.ToString() || userRole == UserRole.SuperAdmin.ToString();

            if (!isAdmin && userRole == UserRole.Seller.ToString())
            {
                var orderCheck = await _orderService.GetOrderByIdAsync(id);
                if (!orderCheck.Success || orderCheck.Data == null)
                    return NotFound(new ApiResponseDto<bool> { Success = false, Message = "Order not found" });

                int currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

                // Сравниваем с SellerId товара, связанного с заказом
                if (orderCheck.Data.Product == null || orderCheck.Data.Product.SellerId != currentUserId)
                    return Forbid();
            }

            var response = await _orderService.UpdateOrderStatusAsync(id, statusUpdateDto);

            if (!response.Success)
            {
                if (response.Message.Contains("not found"))
                    return NotFound(response);

                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Удалить заказ
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponseDto<bool>>> DeleteOrder(int id)
        {
            var response = await _orderService.DeleteOrderAsync(id);

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