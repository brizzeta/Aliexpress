using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public int OrderID { get; set; }
        public Order Order { get; set; } = null!;
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
        public PaymentMethod Method { get; set; }
        public string? TransactionId { get; set; }
    }
}
