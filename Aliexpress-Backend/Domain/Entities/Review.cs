using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public int BuyerID { get; set; }
        public User Buyer { get; set; } = null!;
        public int ProductID { get; set; }
        public Product Product { get; set; } = null!;
        public int SellerID { get; set; }
        public User Seller { get; set; } = null!;
        public decimal rating {  get; set; }

        public string? Comment { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
