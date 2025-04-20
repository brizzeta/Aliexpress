using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Common;
using Application.DTOs.Order;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork uof;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork uof, IMapper mapper)
        {
            this.uof = uof;
            this._mapper = mapper;
        }

        public async Task<ApiResponseDto<OrderDto>> CreateOrderAsync(OrderCreateDto orderCreateDto)
        {
            try
            {
                var product = await uof.Products.GetByIdAsync(orderCreateDto.ProductId);
                if(product == null) 
                    return ApiResponseDto<OrderDto>.FailureResult($"Product with ID {orderCreateDto.ProductId} not found");
                var order = _mapper.Map<Order>(orderCreateDto);
                order.TotalPrice = product.Price * order.Quantity;
                foreach(var item in orderCreateDto.OrderItems)
                {
                    var orderItem = _mapper.Map<OrderItem>(item);
                    var orderProduct = await uof.Products.GetByIdAsync(item.ProductID);
                    if(orderProduct == null)
                        return ApiResponseDto<OrderDto>.FailureResult($"Product with ID {orderCreateDto.ProductId} not found");
                    orderItem.PricePerItem = orderProduct.Price;
                    orderItem.Subtotal = orderItem.PricePerItem * orderItem.Quantity;
                    order.OrderItems.Add(orderItem);
                }
                await uof.Orders.AddAsync(order);
                await uof.CompleteAsync();
                var orderDto = _mapper.Map<OrderDto>(order);
                return ApiResponseDto<OrderDto>.SuccessResult(orderDto, "Order created succesfully");
            }
            catch (Exception ex)
            {
                return ApiResponseDto<OrderDto>.FailureResult("Error creating order", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<bool>> DeleteOrderAsync(int id)
        {
            try
            {
                var order = await uof.Orders.GetByIdAsync(id);
                if (order == null)
                    return ApiResponseDto<bool>.FailureResult($"Order with ID {id} not found");
                uof.Orders.Delete(order);
                await uof.CompleteAsync();
                return ApiResponseDto<bool>.SuccessResult(true, "Order deleted successfully");

            }
            catch (Exception ex)
            {
                return ApiResponseDto<bool>.FailureResult("Error deleting order", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<IEnumerable<OrderDto>>> GetAllOrdersAsync()
        {
            try
            {
                var orders = await uof.Orders.GetAllAsync();
                var orderDto = _mapper.Map<IEnumerable<OrderDto>>(orders);
                return ApiResponseDto<IEnumerable<OrderDto>>.SuccessResult(orderDto);
            }
            catch (Exception ex)
            {
                return ApiResponseDto<IEnumerable<OrderDto>>.FailureResult("Error retrieving all orders", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<OrderDetailDto>> GetOrderByIdAsync(int id)
        {
            try
            {
                var order = await uof.Orders.GetByIdAsync(id);
                if (order == null)
                    return ApiResponseDto<OrderDetailDto>.FailureResult($"Order with ID {id} not found");
                var orderDto = _mapper.Map<OrderDetailDto>(order);
                return ApiResponseDto<OrderDetailDto>.SuccessResult(orderDto);
            }
            catch (Exception ex)
            {
                return ApiResponseDto<OrderDetailDto>.FailureResult("Error retrieving order", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<IEnumerable<OrderDto>>> GetOrdersByBuyerIdAsync(int buyerId)
        {
            try
            {
                var order = await uof.Orders.FindAsync(i => i.BuyerId == buyerId);
                var orderDtos = _mapper.Map<IEnumerable<OrderDto>>(order);
                return ApiResponseDto<IEnumerable<OrderDto>>.SuccessResult(orderDtos);
            }
            catch (Exception ex)
            {
                return ApiResponseDto<IEnumerable<OrderDto>>.FailureResult("Error retrieving order", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<IEnumerable<OrderDto>>> GetOrdersForProductAsync(int productId)
        {
            try
            {
                var order = await uof.Orders.FindAsync(i => i.ProductId == productId);
                var orderDtos = _mapper.Map<IEnumerable<OrderDto>>(order);
                return ApiResponseDto<IEnumerable<OrderDto>>.SuccessResult(orderDtos);
            }
            catch (Exception ex)
            {
                return ApiResponseDto<IEnumerable<OrderDto>>.FailureResult("Error retrieving order", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<bool>> UpdateOrderAsync(int orderId, OrderUpdateDto orderUpdateDto)
        {
            try
            {
                var order = await uof.Orders.GetByIdAsync(orderId);
                if (order == null)
                    return ApiResponseDto<bool>.FailureResult($"Order with ID {orderId} not found");
                if (orderUpdateDto.Status.HasValue)
                    order.Status = orderUpdateDto.Status.Value;
                if (orderUpdateDto.ShippingAddress != null)
                    order.ShippingAddress = orderUpdateDto.ShippingAddress;
                order.UpdatedAt = DateTime.UtcNow;
                uof.Orders.Update(order);
                await uof.CompleteAsync();
                return ApiResponseDto<bool>.SuccessResult(true, "Order updated successfully");
            }
            catch (Exception ex)
            {
                return ApiResponseDto<bool>.FailureResult("Error updating order", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<bool>> UpdateOrderStatusAsync(int orderId, OrderStatusUpdateDto statusUpdateDto)
        {
            try
            {
                var order = await uof.Orders.GetByIdAsync(orderId);
                if (order == null)
                    return ApiResponseDto<bool>.FailureResult($"Order with ID {orderId} not found");
                order.Status = statusUpdateDto.Status;
                order.UpdatedAt = DateTime.UtcNow;
                uof.Orders.Update(order);
                await uof.CompleteAsync();
                return ApiResponseDto<bool>.SuccessResult(true, "Order status updated successfully");
            }
            catch (Exception ex)
            {
                return ApiResponseDto<bool>.FailureResult("Error updating order status", new List<string> { ex.Message });
            }
        }
    }
}
