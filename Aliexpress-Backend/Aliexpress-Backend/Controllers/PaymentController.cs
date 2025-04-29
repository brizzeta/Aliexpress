using System.Security.Claims;
using Application.DTOs.Common;
using Application.DTOs.Order;
using Application.Interfaces;
using Application.Services;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aliexpress_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IOrderService _orderService;

        public PaymentController(IPaymentService paymentService, IOrderService orderService)
        {
            _paymentService = paymentService;
            _orderService = orderService;
        }

        /// <summary>
        /// Получить все платежи (только для администраторов)
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<PaymentDto>>>> GetAllPayments()
        {
            var response = await _paymentService.GetAllPaymentsAsync();

            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }

        /// <summary>
        /// Получить платеж по ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponseDto<PaymentDto>>> GetPaymentById(int id)
        {
            var response = await _paymentService.GetPaymentByIdAsync(id);

            if (!response.Success)
                return BadRequest(response);

            if (response.Data == null)
                return NotFound(response);

            // Авторизация
            string userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            int currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            bool isAdmin = userRole == UserRole.Admin.ToString() || userRole == UserRole.SuperAdmin.ToString();

            if (!isAdmin)
            {
                if (userRole == UserRole.Buyer.ToString())
                {
                    // Только свои платежи
                    if (response.Data.BuyerId != currentUserId)
                        return Forbid();
                }
                else if (userRole == UserRole.Seller.ToString())
                {
                    // Только если продавец этого товара
                    if (response.Data.SellerId != currentUserId)
                        return Forbid();
                }
            }

            return Ok(response);
        }

        /// <summary>
        /// Получить платежи текущего покупателя
        /// </summary>
        [HttpGet("my")]
        [Authorize(Roles = "Buyer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<PaymentDto>>>> GetMyPayments()
        {
            int buyerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var response = await _paymentService.GetPaymentsByBuyerIdAsync(buyerId);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        /// <summary>
        /// Получить платежи по ID покупателя (для администраторов)
        /// </summary>
        [HttpGet("buyer/{buyerId}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<PaymentDto>>>> GetPaymentsByBuyerId(int buyerId)
        {
            var response = await _paymentService.GetPaymentsByBuyerIdAsync(buyerId);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        /// <summary>
        /// Получить платежи по ID заказа
        /// </summary>
        [Authorize]
        [HttpGet("order/{orderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<PaymentDto>>>> GetPaymentsByOrderId(int orderId)
        {
            string userRole = User.FindFirst(ClaimTypes.Role)?.Value!;
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            bool isAdmin = userRole == UserRole.Admin.ToString() || userRole == UserRole.SuperAdmin.ToString();

            if (!isAdmin)
            {
                // Получаем заказ (с продуктом), чтобы проверить права
                var orderCheck = await _orderService.GetOrderByIdAsync(orderId);
                if (!orderCheck.Success || orderCheck.Data == null)
                {
                    return NotFound(new ApiResponseDto<IEnumerable<PaymentDto>>
                    {
                        Success = false,
                        Message = "Order not found"
                    });
                }

                bool isBuyer = userRole == UserRole.Buyer.ToString() && orderCheck.Data.BuyerId == userId;
                bool isSeller = userRole == UserRole.Seller.ToString()
                    && orderCheck.Data.Product?.SellerId == userId;

                if (!isBuyer && !isSeller)
                    return Forbid();
            }

            var response = await _paymentService.GetPaymentsByOrderIdAsync(orderId);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }


        /// <summary>
        /// Проверить статус платежа по ID транзакции
        /// </summary>
        [HttpGet("status/{transactionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponseDto<PaymentStatus>>> CheckPaymentStatus(string transactionId)
        {
            // Для проверки статуса транзакции нужно сначала найти платеж по transactionId
            // и затем проверить права доступа
            // Этот функционал требует соответствующей реализации в сервисе

            var response = await _paymentService.CheckPaymentStatusAsync(transactionId);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        /// <summary>
        /// Получить общую сумму платежей за период (только для администраторов)
        /// </summary>
        [HttpGet("total")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponseDto<decimal>>> GetTotalPaymentsForPeriod([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var response = await _paymentService.GetTotalPaymentsForPeriodAsync(startDate, endDate);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        /// <summary>
        /// Создать новый платеж
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Buyer")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponseDto<PaymentDto>>> CreatePayment([FromBody] PaymentCreateDto paymentCreateDto)
        {
            // Установка ID покупателя из токена
            int buyerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            // Проверка, что заказ принадлежит этому покупателю
            var orderCheck = await _orderService.GetOrderByIdAsync(paymentCreateDto.OrderID);
            if (!orderCheck.Success || orderCheck.Data == null)
                return BadRequest(new ApiResponseDto<PaymentDto> { Success = false, Message = "Order not found" });

            if (orderCheck.Data.BuyerId != buyerId)
                return Forbid();

            var response = await _paymentService.CreatePaymentAsync(paymentCreateDto);
            if (!response.Success)
                return BadRequest(response);

            return CreatedAtAction(nameof(GetPaymentById), new { id = response.Data.Id }, response);
        }

        /// <summary>
        /// Обработать платеж
        /// </summary>
        [HttpPost("process")]
        [Authorize(Roles = "Buyer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponseDto<bool>>> ProcessPayment([FromBody] PaymentCreateDto paymentCreateDto)
        {
            // Установка ID покупателя из токена
            int buyerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            // Проверка, что заказ принадлежит этому покупателю
            var orderCheck = await _orderService.GetOrderByIdAsync(paymentCreateDto.OrderID);
            if (!orderCheck.Success || orderCheck.Data == null)
                return BadRequest(new ApiResponseDto<bool> { Success = false, Message = "Order not found" });

            if (orderCheck.Data.BuyerId != buyerId)
                return Forbid();

            var response = await _paymentService.ProcessPaymentAsync(paymentCreateDto);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        /// <summary>
        /// Обновить статус платежа (только для администраторов)
        /// </summary>
        [HttpPut("{id}/status")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponseDto<bool>>> UpdatePaymentStatus(int id, [FromBody] PaymentStatus status)
        {
            var response = await _paymentService.UpdatePaymentStatusAsync(id, status);

            if (!response.Success)
            {
                if (response.Message.Contains("not found"))
                    return NotFound(response);

                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Возврат платежа (для администраторов и продавца по своим заказам)
        /// </summary>
        [HttpPost("{id}/refund")]
        [Authorize(Roles = "Seller,Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponseDto<bool>>> RefundPayment(int id)
        {
            // Проверка прав на возврат платежа
            string userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            bool isAdmin = userRole == UserRole.Admin.ToString() || userRole == UserRole.SuperAdmin.ToString();

            if (!isAdmin)
            {
                // Для продавцов - проверить, что платеж связан с их заказом
                var payment = await _paymentService.GetPaymentByIdAsync(id);
                if (!payment.Success || payment.Data == null)
                    return NotFound(new ApiResponseDto<bool> { Success = false, Message = "Payment not found" });

                int sellerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                if (payment.Data.SellerId != sellerId)
                    return Forbid();
            }

            var response = await _paymentService.RefundPaymentAsync(id);

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
