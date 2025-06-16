using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Application.DTOs.Order
{
    public class PaymentCreateDto
    {
        [Required]
        public int OrderID { get; set; }

        [Required]
        public PaymentMethod PaymentMethod { get; set; }

        public string? TransactionId { get; set; }
    }
}
