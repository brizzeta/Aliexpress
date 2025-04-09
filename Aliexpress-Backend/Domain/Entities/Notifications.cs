using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities
{
    public class Notifications
    {
        public int Id { get; set; }
        public int RecepientID {  get; set; }
        public User User { get; set; } = null!;
        public NotificationType Type { get; set; } = NotificationType.Info;
        public bool IsRead { get; set; } = false;
        public string Message { get; set; } = null!;
    }
}
