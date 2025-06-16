using Application.DTOs.Common;
using Application.DTOs.Order;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Aliexpress_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrderItemsController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;
        private readonly IOrderService _orderService;

        public OrderItemsController(IOrderItemService orderItemService, IOrderService orderService)
        {
            _orderItemService = orderItemService;
            _orderService = orderService;
        }

        [HttpGet("byOrder/{orderId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<OrderItemDto>>>> GetOrderItems(int orderId)
        {
            var currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var orderResponse = await _orderService.GetOrderByIdAsync(orderId);
            if (orderResponse?.Data == null)
            {
                return NotFound(new ApiResponseDto<IEnumerable<OrderItemDto>>
                {
                    Success = false,
                    Message = "Order not found"
                });
            }

            var order = orderResponse.Data;

            if (order.BuyerId != currentUserId && !User.IsInRole("Admin") && !User.IsInRole("Manager"))
                return Forbid();

            var response = await _orderItemService.GetOrderItemsByOrderIdAsync(orderId);

            if (!response.Success)
                return response.Message?.Contains("not found") == true ? NotFound(response) : BadRequest(response);

            return Ok(response);
        }

        [HttpPost("{orderId}")]
        [Authorize(Roles = "Buyer,Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponseDto<OrderItemDto>>> AddOrderItem(int orderId, [FromBody] OrderItemCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var order = await _orderService.GetOrderByIdAsync(orderId);
            if (order?.Data == null)
            {
                return NotFound(new ApiResponseDto<OrderItemDto>
                {
                    Success = false,
                    Message = "Order not found"
                });
            }

            var response = await _orderItemService.AddOrderItemAsync(dto);

            if (!response.Success)
                return BadRequest(response);

            return CreatedAtAction(
                nameof(GetOrderItems),
                new { orderId = response.Data.OrderID },
                response
            );
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Buyer,Admin,SuperAdmin")]
        public async Task<ActionResult<ApiResponseDto<bool>>> DeleteOrderItem(int id)
        {
            var response = await _orderItemService.DeleteOrderItemAsync(id);

            if (!response.Success)
                return BadRequest(response); 

            return Ok(response);
        }
    }
}
