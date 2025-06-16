using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IChatRepository : IGenericRepository<Chat>
    {
        Task<Chat?> GetChatWithMessagesAsync(int chatId);
        Task<IEnumerable<Chat>> GetChatsByUserIdAsync(int userId);
    }
}
