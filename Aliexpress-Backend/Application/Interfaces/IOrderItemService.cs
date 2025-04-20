using Application.DTOs.Common;
using Application.DTOs.Order;

namespace Application.Interfaces
{
    public interface IOrderItemService
    {
        Task<ApiResponseDto<IEnumerable<OrderItemDto>>> GetOrderItemsByOrderIdAsync(int orderId);
        Task<ApiResponseDto<OrderItemDto>> AddOrderItemAsync(OrderItemCreateDto dto);
        Task<ApiResponseDto<bool>> DeleteOrderItemAsync(int id);
    }
}
