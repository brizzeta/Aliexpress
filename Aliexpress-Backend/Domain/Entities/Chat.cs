using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Chat
    {
        public int Id { get; set; }
        public int BuyerID { get; set; }
        public User Buyer { get; set; } = null!;
        public int SellerID { get; set; }
        public User Seller { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Messages> Messages { get; set; } = new List<Messages>();
    }
}
