using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Order
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal PricePerItem { get; set; }
        public decimal Subtotal { get; set; }
    }
}
