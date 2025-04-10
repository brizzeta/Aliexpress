using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Chat
{
    public class MessageCreateDto
    {
        [Required]
        public int ChatID { get; set; }

        [Required]
        public string Message { get; set; } = null!;
    }
}
