using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Review
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public int BuyerID { get; set; }
        public string BuyerName { get; set; } = null!;
        public int ProductID { get; set; }
        public string ProductName { get; set; } = null!;
        public int SellerID { get; set; }
        public string SellerName { get; set; } = null!;
        public decimal Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
