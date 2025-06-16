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

namespace Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork uof;
        private readonly IMapper _mapper;

        public PaymentService(IUnitOfWork uof, IMapper mapper)
        {
            this.uof = uof;
            this._mapper = mapper;
        }

        public async Task<ApiResponseDto<PaymentStatus>> CheckPaymentStatusAsync(string transactionId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(transactionId))
                    return ApiResponseDto<PaymentStatus>.FailureResult("Transaction ID cannot be empty");
                var payment = (await uof.Payments.FindAsync(i => i.TransactionId == transactionId)).FirstOrDefault();
                if (payment == null)
                    return ApiResponseDto<PaymentStatus>.FailureResult($"No payment found with transaction ID {transactionId}");
                return ApiResponseDto<PaymentStatus>.SuccessResult(payment.Status, "Payment status retrieved");
            }
            catch (Exception ex)
            {
                return ApiResponseDto<PaymentStatus>.FailureResult("Error checking payment status", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<PaymentDto>> CreatePaymentAsync(PaymentCreateDto paymentCreateDto)
        {
            try
            {
                var order = await uof.Orders.GetByIdAsync(paymentCreateDto.OrderID);
                if (order == null)
                    return ApiResponseDto<PaymentDto>.FailureResult($"Order with ID {paymentCreateDto.OrderID} not found");
                var payment = _mapper.Map<Payment>(paymentCreateDto);
                payment.PaymentDate = DateTime.UtcNow;
                payment.Status = PaymentStatus.Pending;
                await uof.Payments.AddAsync(payment);
                await uof.CompleteAsync();
                var paymentDto = _mapper.Map<PaymentDto>(payment);
                return ApiResponseDto<PaymentDto>.SuccessResult(paymentDto, "Payment created successfully");
            }
            catch (Exception ex)
            {
                return ApiResponseDto<PaymentDto>.FailureResult("Error creating payment", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<IEnumerable<PaymentDto>>> GetAllPaymentsAsync()
        {
            try
            {
                var payments = await uof.Payments.GetAllAsync();
                var paymentDtos = _mapper.Map<IEnumerable<PaymentDto>>(payments);
                return ApiResponseDto<IEnumerable<PaymentDto>>.SuccessResult(paymentDtos);
            }
            catch (Exception ex)
            {
                return ApiResponseDto<IEnumerable<PaymentDto>>.FailureResult("Error retrieving payments", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<PaymentDto>> GetPaymentByIdAsync(int id)
        {
            try
            {
                var payment = await uof.Payments.GetByIdAsync(id);
                if (payment == null)
                    return ApiResponseDto<PaymentDto>.FailureResult($"Payment with ID {id} not found");
                var paymentDto = _mapper.Map<PaymentDto>(payment);
                return ApiResponseDto<PaymentDto>.SuccessResult(paymentDto);
            }
            catch (Exception ex)
            {
                return ApiResponseDto<PaymentDto>.FailureResult("Error retrieving payment", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<IEnumerable<PaymentDto>>> GetPaymentsByBuyerIdAsync(int buyerId)
        {
            try
            {
                var buyerExists = await uof.Users.GetByIdAsync(buyerId) != null;
                if (!buyerExists)
                    return ApiResponseDto<IEnumerable<PaymentDto>>.FailureResult($"Buyer with ID {buyerId} not found");
                var buyerOrders = await uof.Orders.FindAsync(o => o.BuyerId == buyerId);
                if (!buyerOrders.Any())
                    return ApiResponseDto<IEnumerable<PaymentDto>>.SuccessResult(new List<PaymentDto>(), "No orders found for this buyer");
                var orderIds = buyerOrders.Select(o => o.Id).ToList();
                var payments = await uof.Payments.FindAsync(p => orderIds.Contains(p.OrderID));
                var paymentDtos = _mapper.Map<IEnumerable<PaymentDto>>(payments);
                return ApiResponseDto<IEnumerable<PaymentDto>>.SuccessResult(paymentDtos);
            }
            catch (Exception ex)
            {
                return ApiResponseDto<IEnumerable<PaymentDto>>.FailureResult("Error retrieving buyer payments", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<IEnumerable<PaymentDto>>> GetPaymentsByOrderIdAsync(int orderId)
        {
            try
            {
                var order = await uof.Orders.GetByIdAsync(orderId);
                if (order == null)
                    return ApiResponseDto<IEnumerable<PaymentDto>>.FailureResult($"Order with ID {orderId} not found");
                var payments = await uof.Payments.FindAsync(p => p.OrderID == orderId);
                var paymentDtos = _mapper.Map<IEnumerable<PaymentDto>>(payments);
                return ApiResponseDto<IEnumerable<PaymentDto>>.SuccessResult(paymentDtos);
            }
            catch (Exception ex)
            {
                return ApiResponseDto<IEnumerable<PaymentDto>>.FailureResult("Error retrieving order payments", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<decimal>> GetTotalPaymentsForPeriodAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                if (startDate > endDate)
                    return ApiResponseDto<decimal>.FailureResult("Start date must be before end date");
                var paymentsPeriod = await uof.Payments.FindAsync(
                    p => p.PaymentDate >= startDate &&
                         p.PaymentDate <= endDate &&
                         p.Status == PaymentStatus.Completed);
                decimal total = 0;
                foreach (var payment in paymentsPeriod)
                {
                    var order = await uof.Orders.GetByIdAsync(payment.OrderID);
                    if (order != null)
                        total += order.TotalPrice;
                }
                return ApiResponseDto<decimal>.SuccessResult(total, $"Total payments between {startDate:d} and {endDate:d}");
            }
            catch (Exception ex)
            {
                return ApiResponseDto<decimal>.FailureResult("Error calculating total payments", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<bool>> ProcessPaymentAsync(PaymentCreateDto paymentCreateDto)
        {
            try
            {
                var order = await uof.Orders.GetByIdAsync(paymentCreateDto.OrderID);
                if (order == null)
                    return ApiResponseDto<bool>.FailureResult($"Order with ID {paymentCreateDto.OrderID} not found");

                var payment = _mapper.Map<Payment>(paymentCreateDto);
                payment.PaymentDate = DateTime.UtcNow;
                payment.Status = PaymentStatus.Pending;

                await uof.Payments.AddAsync(payment);
                /*
                 * Здесь должна быть логика обработки платежа (пока скип)
                 */
                payment.Status = PaymentStatus.Completed;

                order.Status = OrderStatus.Confirmed;
                order.UpdatedAt = DateTime.UtcNow;
                uof.Orders.Update(order);

                await uof.CompleteAsync();
                return ApiResponseDto<bool>.SuccessResult(true, "Payment processed successfully");
            }
            catch (Exception ex)
            {
                return ApiResponseDto<bool>.FailureResult("Error processing payment", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<bool>> RefundPaymentAsync(int paymentId)
        {
            try
            {
                var payment = await uof.Payments.GetByIdAsync(paymentId);

                if (payment == null)
                    return ApiResponseDto<bool>.FailureResult($"Payment with ID {paymentId} not found");

                if (payment.Status != PaymentStatus.Completed)
                    return ApiResponseDto<bool>.FailureResult("Only completed payments can be refunded");

                payment.Status = PaymentStatus.Refunded;
                uof.Payments.Update(payment);

                var order = await uof.Orders.GetByIdAsync(payment.OrderID);
                if (order != null)
                {
                    order.Status = OrderStatus.Refunded;
                    order.UpdatedAt = DateTime.UtcNow;
                    uof.Orders.Update(order);
                }

                await uof.CompleteAsync();
                return ApiResponseDto<bool>.SuccessResult(true, "Payment refunded successfully");
            }
            catch (Exception ex)
            {
                return ApiResponseDto<bool>.FailureResult("Error refunding payment", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<bool>> UpdatePaymentStatusAsync(int paymentId, PaymentStatus status)
        {
            try
            {
                var payment = await uof.Payments.GetByIdAsync(paymentId);
                if (payment == null)
                    return ApiResponseDto<bool>.FailureResult($"Payment with ID {paymentId} not found");
                payment.Status = status;
                uof.Payments.Update(payment);
                await uof.CompleteAsync();

                return ApiResponseDto<bool>.SuccessResult(true, "Payment status updated successfully");
            }
            catch (Exception ex)
            {
                return ApiResponseDto<bool>.FailureResult("Error updating payment status", new List<string> { ex.Message });
            }
        }
    }
}
