using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Application.DTOs.Order
{
    public class PaymentDto
    {
        public int Id { get; set; }
        public int OrderID { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentStatus Status { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string? TransactionId { get; set; }
        public int BuyerId { get; set; }      
        public int SellerId { get; set; }
    }
}
