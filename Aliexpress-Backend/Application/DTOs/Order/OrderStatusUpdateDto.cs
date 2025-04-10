using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Application.DTOs.Order
{
    public class OrderStatusUpdateDto
    {
        [Required]
        public OrderStatus Status { get; set; }
    }
}
