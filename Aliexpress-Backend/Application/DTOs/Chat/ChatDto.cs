using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Chat
{
    public class ChatDto
    {
        public int Id { get; set; }
        public int BuyerID { get; set; }
        public string BuyerName { get; set; } = null!;
        public int SellerID { get; set; }
        public string SellerName { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public List<MessageDto> Messages { get; set; } = new List<MessageDto>();
        public MessageDto? LastMessage { get; set; }
    }
}
