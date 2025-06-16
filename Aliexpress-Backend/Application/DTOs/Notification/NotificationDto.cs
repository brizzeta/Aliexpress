using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Application.DTOs.Notification
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public int RecepientID { get; set; }
        public string RecepientName { get; set; } = null!;
        public NotificationType Type { get; set; }
        public bool IsRead { get; set; }
        public string Message { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
    }
}
