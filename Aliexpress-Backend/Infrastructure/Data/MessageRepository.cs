using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class MessageRepository : GenericRepository<Messages>, IMessageRepository
    {
        public MessageRepository(KlikavaDbContext context) : base(context) { }

        public async Task<IEnumerable<Messages>> GetMessagesByChatIdAsync(int chatId)
        {
            return await _context.Messages
                .Where(m => m.ChatID == chatId)
                .OrderBy(m => m.CreatedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Messages>> GetRecentMessagesAsync(int chatId, int count)
        {
            return await _context.Messages
                .Where(m => m.ChatID == chatId)
                .OrderByDescending(m => m.CreatedDate)
                .Take(count)
                .ToListAsync();
        }
    }
}
