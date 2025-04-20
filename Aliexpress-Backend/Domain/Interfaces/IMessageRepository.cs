using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IMessageRepository : IGenericRepository<Messages>
    {
        Task<IEnumerable<Messages>> GetMessagesByChatIdAsync(int chatId);
        Task<IEnumerable<Messages>> GetRecentMessagesAsync(int chatId, int count);
    }
}
