using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Order
{
    public class ProductSimpleDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public decimal? Discount { get; set; }
        public string? ImageUrl { get; set; }
        public int SellerId { get; set; }
        public string SellerName { get; set; } = null!;
    }
}
