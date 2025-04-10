using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Application.DTOs.Notification
{
    public class NotificationCreateDto
    {
        [Required]
        public int RecepientID { get; set; }

        public NotificationType Type { get; set; } = NotificationType.Info;

        [Required]
        public string Message { get; set; } = null!;
    }
}
