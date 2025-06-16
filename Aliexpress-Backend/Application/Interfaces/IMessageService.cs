using Application.DTOs.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IMessageService
    {
        Task<IEnumerable<MessageDto>> GetMessagesByChatIdAsync(int chatId);
        Task<IEnumerable<MessageDto>> GetRecentMessagesAsync(int chatId, int count);
        Task AddMessageAsync(MessageCreateDto messageDto);
    }
}
