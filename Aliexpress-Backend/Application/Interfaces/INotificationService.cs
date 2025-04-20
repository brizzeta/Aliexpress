using Application.DTOs.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface INotificationService
    {
        Task<IEnumerable<NotificationDto>> GetUnreadNotificationsAsync(int userId);
        Task MarkAllAsReadAsync(int userId);
        Task AddNotificationAsync(NotificationCreateDto notificationDto);
    }
}
