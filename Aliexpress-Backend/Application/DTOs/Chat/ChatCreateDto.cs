using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Chat
{
    public class ChatCreateDto
    {
        [Required]
        public int BuyerID { get; set; }

        [Required]
        public int SellerID { get; set; }

        [Required]
        public string InitialMessage { get; set; } = null!;
    }
}
