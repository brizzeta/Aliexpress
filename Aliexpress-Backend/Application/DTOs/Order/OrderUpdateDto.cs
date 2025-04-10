using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Application.DTOs.Order
{
    public class OrderUpdateDto
    {
        public OrderStatus? Status { get; set; }
        public string? ShippingAddress { get; set; }
    }
}
