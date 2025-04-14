using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;

namespace Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork uof;

        public PaymentService(IUnitOfWork uof)
        {
            this.uof = uof;
        }
        public async Task<PaymentStatus> CheckPaymentStatusAsync(string transactionId)
        {
            var payment = (await uof.Payments.FindAsync(i => i.TransactionId == transactionId)).FirstOrDefault();
            return payment?.Status ?? PaymentStatus.Pending;
        }

        public async Task<Payment> CreatePaymentAsync(Payment payment)
        {
            await uof.Payments.AddAsync(payment);
            await uof.CompleteAsync();
            return payment;
        }

        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
        {
            return await uof.Payments.GetAllAsync();
        }

        public async Task<Payment> GetPaymentByIdAsync(int id)
        {
            return await uof.Payments.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByBuyerIdAsync(int buyerId)
        {
            var buyerOrders = await uof.Orders.FindAsync(i => i.BuyerId == buyerId);
            var ordersId = buyerOrders.Select(i => i.Id).ToList();
            return await uof.Payments.FindAsync(i => ordersId.Contains(i.Id));
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByOrderIdAsync(int orderId)
        {
            return await uof.Payments.FindAsync(i => i.OrderID == orderId);
        }

        public async Task<decimal> GetTotalPaymentsForPeriodAsync(DateTime startDate, DateTime endDate)
        {
            var paymentsPeriod = await uof.Payments.FindAsync(i => i.PaymentDate >= startDate && i.PaymentDate <= endDate && i.Status == PaymentStatus.Completed);
            decimal total = 0;
            foreach(var payment in paymentsPeriod)
            {
                var order = await uof.Orders.GetByIdAsync(payment.OrderID);
                if (order != null)
                    total += order.TotalPrice;
            }
            return total;
        }

        public async Task<bool> ProcessPaymentAsync(int orderId, PaymentMethod method, string transactionId)
        {
            var order = await uof.Orders.GetByIdAsync(orderId);
            if (order == null)
                return false;
            var payment = new Payment
            {
                OrderID = orderId,
                PaymentMethod = method,
                TransactionId = transactionId,
                Status = PaymentStatus.Pending
            };
            await uof.Payments.AddAsync(payment);
            /*
             * здесь должна быть логика платежа (пока скип)
             */
            payment.Status = PaymentStatus.Completed;

            order.Status = OrderStatus.Confirmed; // ??
            uof.Orders.Update(order);
            await uof.CompleteAsync();
            return true;

        }

        public async Task<bool> RefundPaymentAsync(int paymentId)
        {
            var payment = await uof.Payments.GetByIdAsync(paymentId);
            if(payment == null || payment.Status != PaymentStatus.Completed) 
                return false;
            payment.Status = PaymentStatus.Refunded;
            uof.Payments.Update(payment);
            var order = await uof.Orders.GetByIdAsync(payment.OrderID);
            if(order != null)
            {
                order.Status = OrderStatus.Refunded;
                order.UpdatedAt = DateTime.UtcNow;
                uof.Orders.Update(order);
            }
            await uof.CompleteAsync(); 
            return true;
        }

        public async Task UpdatePaymentStatusAsync(int paymentId, PaymentStatus status)
        {
            var payment = await uof.Payments.GetByIdAsync(paymentId);
            if (payment != null)
            {
                payment.Status = status;
                uof.Payments.Update(payment);
                await uof.CompleteAsync();
            }
        }
    }
}
