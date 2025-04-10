using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Chat
{
    public class MessageDto
    {
        public int Id { get; set; }
        public int ChatID { get; set; }
        public int SenderId { get; set; }
        public string SenderName { get; set; } = null!;
        public string Message { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
    }
}
