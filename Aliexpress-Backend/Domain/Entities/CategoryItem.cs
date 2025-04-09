using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CategoryItem
    {
        public int Id { get; set; }
        public int OrderID { get; set; }
        public Order Order { get; set; } = null!;
        public int ProductID {  get; set; }
        public Product Product { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal PricePerItem { get; set; }
        public decimal Subtotal { get; set; }
    }
}
