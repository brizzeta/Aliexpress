using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface INotificationRepository : IGenericRepository<Notifications>
    {
        Task<IEnumerable<Notifications>> GetUnreadNotificationsByUserIdAsync(int userId);
        Task MarkAllAsReadAsync(int userId);
    }
}
