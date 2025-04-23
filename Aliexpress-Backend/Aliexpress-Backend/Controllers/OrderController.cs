using Application.DTOs.Common;
using Application.DTOs.Order;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Aliexpress_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Получить все заказы
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponseDto<OrderDetailDto>>> GetOrderById(int id)
        {
            var response = await _orderService.GetOrderByIdAsync(id);

            if (!response.Success)
                return BadRequest(response);

            if (response.Data == null)
                return NotFound(response);

            return Ok(response);
        }

        /// <summary>
        /// Получить заказы по ID покупателя
        /// </summary>
        [HttpGet("buyer/{buyerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<OrderDto>>>> GetOrdersForProduct(int productId)
        {
            var response = await _orderService.GetOrdersForProductAsync(productId);

            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }

        /// <summary>
        /// Создать новый заказ
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponseDto<OrderDto>>> CreateOrder([FromBody] OrderCreateDto orderCreateDto)
        {
            var response = await _orderService.CreateOrderAsync(orderCreateDto);

            if (!response.Success)
                return BadRequest(response);

            return CreatedAtAction(nameof(GetOrderById), new { id = response.Data.Id }, response);
        }

        /// <summary>
        /// Обновить заказ
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponseDto<bool>>> UpdateOrder(int id, [FromBody] OrderUpdateDto orderUpdateDto)
        {
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponseDto<bool>>> UpdateOrderStatus(int id, [FromBody] OrderStatusUpdateDto statusUpdateDto)
        {
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
