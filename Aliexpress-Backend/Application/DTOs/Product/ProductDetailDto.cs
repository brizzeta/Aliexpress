using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Product
{
    public class ProductDetailDto : ProductDto
    {
        public List<ProductReviewDto> Reviews { get; set; } = new List<ProductReviewDto>();
        public string? SellerEmail { get; set; }
        public string? SellerPhone { get; set; }
        public double SellerRating { get; set; }
    }
}
