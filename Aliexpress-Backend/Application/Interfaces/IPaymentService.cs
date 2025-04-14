using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces
{
    public interface IPaymentService
    {
        Task<Payment> GetPaymentByIdAsync(int id);
        Task<IEnumerable<Payment>> GetAllPaymentsAsync();
        Task<Payment> CreatePaymentAsync(Payment payment);
        Task<IEnumerable<Payment>> GetPaymentsByOrderIdAsync(int orderId);
        Task<IEnumerable<Payment>> GetPaymentsByBuyerIdAsync(int buyerId);
        Task UpdatePaymentStatusAsync(int paymentId, PaymentStatus status);
        Task<bool> ProcessPaymentAsync(int orderId, PaymentMethod method, string transactionId);
        Task<PaymentStatus> CheckPaymentStatusAsync(string transactionId);
        Task<decimal> GetTotalPaymentsForPeriodAsync(DateTime startDate, DateTime endDate);
        Task<bool> RefundPaymentAsync(int paymentId);
    }
}
