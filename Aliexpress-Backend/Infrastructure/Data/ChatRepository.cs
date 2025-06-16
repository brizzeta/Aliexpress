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
    public class ChatRepository : GenericRepository<Chat>, IChatRepository
    {
        public ChatRepository(KlikavaDbContext context) : base(context) { }

        public async Task<Chat?> GetChatWithMessagesAsync(int chatId)
        {
            return await _context.Chats
                .Include(c => c.Messages)
                .FirstOrDefaultAsync(c => c.Id == chatId);
        }

        public async Task<IEnumerable<Chat>> GetChatsByUserIdAsync(int userId)
        {
            return await _context.Chats
                .Where(c => c.BuyerID == userId || c.SellerID == userId)
                .ToListAsync();
        }
    }
}
