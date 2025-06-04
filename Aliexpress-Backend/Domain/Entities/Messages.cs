using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Messages
    {
        public int Id { get; set; }
        public int ChatID { get; set; }
        public Chat Chat { get; set; } = null!;
        public int SenderId { get; set; }
        public User Sender { get; set; } = null!;
        public string Message { get; set; } = null!;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
