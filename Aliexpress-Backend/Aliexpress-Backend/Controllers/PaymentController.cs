using Application.DTOs.Common;
using Application.DTOs.Order;
using Application.Interfaces;
using Application.Services;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Aliexpress_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        /// <summary>
        /// Получить все платежи
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponseDto<PaymentDto>>> GetPaymentById(int id)
        {
            var response = await _paymentService.GetPaymentByIdAsync(id);

            if (!response.Success)
                return BadRequest(response);

            if (response.Data == null)
                return NotFound(response);

            return Ok(response);
        }

        /// <summary>
        /// Получить платежи по ID покупателя
        /// </summary>
        [HttpGet("buyer/{buyerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        [HttpGet("order/{orderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<PaymentDto>>>> GetPaymentsByOrderId(int orderId)
        {
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponseDto<PaymentStatus>>> CheckPaymentStatus(string transactionId)
        {
            var response = await _paymentService.CheckPaymentStatusAsync(transactionId);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        /// <summary>
        /// Получить общую сумму платежей за период
        /// </summary>
        [HttpGet("total")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponseDto<PaymentDto>>> CreatePayment([FromBody] PaymentCreateDto paymentCreateDto)
        {
            var response = await _paymentService.CreatePaymentAsync(paymentCreateDto);

            if (!response.Success)
                return BadRequest(response);

            return CreatedAtAction(nameof(GetPaymentById), new { id = response.Data.Id }, response);
        }

        /// <summary>
        /// Обработать платеж
        /// </summary>
        [HttpPost("process")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponseDto<bool>>> ProcessPayment([FromBody] PaymentCreateDto paymentCreateDto)
        {
            var response = await _paymentService.ProcessPaymentAsync(paymentCreateDto);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        /// <summary>
        /// Обновить статус платежа
        /// </summary>
        [HttpPut("{id}/status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        /// Возврат платежа
        /// </summary>
        [HttpPost("{id}/refund")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponseDto<bool>>> RefundPayment(int id)
        {
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
