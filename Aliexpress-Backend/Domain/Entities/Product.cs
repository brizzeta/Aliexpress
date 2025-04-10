using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public int SellerId { get; set; }
        public User Seller { get; set; } = null!;

        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public decimal Price { get; set; }
        public decimal? Discount { get; set; }

        public int StockQuantity { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public List<string> ImageUrls { get; set; } = new();

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }

        public double? Rating { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<Order> Order { get; set; } = new List<Order>();
        public ICollection<OrderItem> OrderItem { get; set; } = new List<OrderItem>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();

    }
}
