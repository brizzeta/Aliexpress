using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces
{
    public interface IOrderService
    {
        Task<Order> GetOrderByIdAsync(int id);
        Task<IEnumerable<Order>> GetOrdersByBuyerIdAsync(int buyerId);
        Task<IEnumerable<Order>> GetOrdersForProductAsync(int productId);
        Task<Order> CreateOrderAsync(Order order);
        Task UpdateOrderStatusAsync(int orderId, OrderStatus status);
        Task<bool> DeleteOrderAsync(int id);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
    }
}
