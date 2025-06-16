using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Product
{
    public class ProductUpdateDto
    {
        [StringLength(200)]
        public string? Name { get; set; }

        public string? Description { get; set; }

        [Range(0.01, 1000000)]
        public decimal? Price { get; set; }

        [Range(0, 100)]
        public decimal? Discount { get; set; }

        [Range(0, int.MaxValue)]
        public int? StockQuantity { get; set; }

        public int? CategoryId { get; set; }

        public List<string>? ImageUrls { get; set; }

        public bool? IsActive { get; set; }
    }

}
