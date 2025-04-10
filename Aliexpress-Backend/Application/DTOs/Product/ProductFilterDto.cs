using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Product
{
    public class ProductFilterDto
    {
        public string? SearchTerm { get; set; }
        public int? CategoryId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? SellerId { get; set; }
        public bool? InStock { get; set; }
        public bool? HasDiscount { get; set; }
        public double? MinRating { get; set; }
        public string? SortBy { get; set; } // Price, Rating, Newest
        public bool SortAscending { get; set; } = true;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
