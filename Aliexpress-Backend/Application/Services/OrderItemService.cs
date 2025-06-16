using Application.DTOs.Common;
using Application.DTOs.Order;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IUnitOfWork uof;
        private readonly IMapper _mapper;

        public OrderItemService(IUnitOfWork uof, IMapper mapper)
        {
            this.uof = uof;
            this._mapper = mapper;
        }

        public async Task<ApiResponseDto<IEnumerable<OrderItemDto>>> GetOrderItemsByOrderIdAsync(int orderId)
        {
            var items = await uof.OrderItems.FindAsync(i => i.OrderID == orderId);
            return ApiResponseDto<IEnumerable<OrderItemDto>>.SuccessResult(_mapper.Map<IEnumerable<OrderItemDto>>(items));
        }

        public async Task<ApiResponseDto<OrderItemDto>> AddOrderItemAsync(OrderItemCreateDto dto)
        {
            var item = _mapper.Map<OrderItem>(dto);
            item.Subtotal = item.Quantity * item.PricePerItem;

            await uof.OrderItems.AddAsync(item);
            await uof.CompleteAsync();

            return ApiResponseDto<OrderItemDto>.SuccessResult(_mapper.Map<OrderItemDto>(item), "Item added");
        }

        public async Task<ApiResponseDto<bool>> DeleteOrderItemAsync(int id)
        {
            var item = await uof.OrderItems.GetByIdAsync(id);
            if (item == null)
                return ApiResponseDto<bool>.FailureResult("Item not found");

            uof.OrderItems.Delete(item);
            await uof.CompleteAsync();

            return ApiResponseDto<bool>.SuccessResult(true, "Item deleted");
        }
    }

}
