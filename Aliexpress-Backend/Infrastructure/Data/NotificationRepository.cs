using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class NotificationRepository : GenericRepository<Notifications>, INotificationRepository
    {
        public NotificationRepository(KlikavaDbContext context) : base(context) { }

        public async Task<IEnumerable<Notifications>> GetUnreadNotificationsByUserIdAsync(int userId)
        {
            return await _context.Notifications
                .Where(n => n.RecepientID == userId && !n.IsRead)
                .ToListAsync();
        }

        public async Task MarkAllAsReadAsync(int userId)
        {
            var notifications = await _context.Notifications
                .Where(n => n.RecepientID == userId && !n.IsRead)
                .ToListAsync();

            notifications.ForEach(n => n.IsRead = true);
            await _context.SaveChangesAsync();
        }
    }
}
