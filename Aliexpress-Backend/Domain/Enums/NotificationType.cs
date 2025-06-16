using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum NotificationType
    {
        Info = 1,
        Warning = 2,
        Success = 3,
        Error = 4,
        OrderUpdate = 5,
        PaymentUpdate = 6,
        Message = 7
    }
}
