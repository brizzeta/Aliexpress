using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork uof;

        public OrderService(IUnitOfWork uof)
        {
            this.uof = uof;
        }
        public async Task<Order> CreateOrderAsync(Order order)
        {
            await uof.Orders.AddAsync(order);
            await uof.CompleteAsync();
            return order;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await uof.Orders.GetByIdAsync(id);
            if (order == null)
                return false;
            uof.Orders.Delete(order);
            await uof.CompleteAsync();
            return true;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await uof.Orders.GetAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await uof.Orders.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Order>> GetOrdersByBuyerIdAsync(int buyerId)
        {
            return await uof.Orders.FindAsync(i => i.BuyerId == buyerId);
        }

        public async Task<IEnumerable<Order>> GetOrdersForProductAsync(int productId)
        {
            return await uof.Orders.FindAsync(i => i.ProductId == productId);
        }

        public async Task UpdateOrderStatusAsync(int orderId, OrderStatus status)
        {
            var order = await uof.Orders.GetByIdAsync(orderId);
            if (order != null)
            {
                order.Status = status;
                order.UpdatedAt = DateTime.UtcNow;
                uof.Orders.Update(order);
                await uof.CompleteAsync();
            }
        }
    }
}
