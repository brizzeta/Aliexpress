using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Product
{
    public class ProductCreateDto
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        [Required]
        [Range(0.01, 1000000)]
        public decimal Price { get; set; }

        [Range(0, 100)]
        public decimal? Discount { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int SellerId { get; set; }

        public List<string> ImageUrls { get; set; } = new();

        public bool IsActive { get; set; } = true;
    }
}
