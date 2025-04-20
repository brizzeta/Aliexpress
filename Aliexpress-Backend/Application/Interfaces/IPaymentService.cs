using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Common;
using Application.DTOs.Order;
using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces
{
    public interface IPaymentService
    {
        Task<ApiResponseDto<PaymentDto>> GetPaymentByIdAsync(int id);
        Task<ApiResponseDto<IEnumerable<PaymentDto>>> GetAllPaymentsAsync();
        Task<ApiResponseDto<PaymentDto>> CreatePaymentAsync(PaymentCreateDto paymentCreateDto);
        Task<ApiResponseDto<IEnumerable<PaymentDto>>> GetPaymentsByOrderIdAsync(int orderId);
        Task<ApiResponseDto<IEnumerable<PaymentDto>>> GetPaymentsByBuyerIdAsync(int buyerId);
        Task<ApiResponseDto<bool>> UpdatePaymentStatusAsync(int paymentId, PaymentStatus status);
        Task<ApiResponseDto<bool>> ProcessPaymentAsync(PaymentCreateDto paymentCreateDto);
        Task<ApiResponseDto<PaymentStatus>> CheckPaymentStatusAsync(string transactionId);
        Task<ApiResponseDto<decimal>> GetTotalPaymentsForPeriodAsync(DateTime startDate, DateTime endDate);
        Task<ApiResponseDto<bool>> RefundPaymentAsync(int paymentId);
    }
}
