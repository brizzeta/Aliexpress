using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Common;
using Application.DTOs.Order;

namespace Application.Interfaces
{
    public interface IOrderService
    {
        Task<ApiResponseDto<OrderDetailDto>> GetOrderByIdAsync(int id);
        Task<ApiResponseDto<IEnumerable<OrderDto>>> GetOrdersByBuyerIdAsync(int buyerId);
        Task<ApiResponseDto<IEnumerable<OrderDto>>> GetOrdersForProductAsync(int productId);
        Task<ApiResponseDto<OrderDto>> CreateOrderAsync(OrderCreateDto orderCreateDto);
        Task<ApiResponseDto<bool>> UpdateOrderStatusAsync(int orderId, OrderStatusUpdateDto statusUpdateDto);
        Task<ApiResponseDto<bool>> UpdateOrderAsync(int orderId, OrderUpdateDto orderUpdateDto);
        Task<ApiResponseDto<bool>> DeleteOrderAsync(int id);
        Task<ApiResponseDto<IEnumerable<OrderDto>>> GetAllOrdersAsync();
    }
}
