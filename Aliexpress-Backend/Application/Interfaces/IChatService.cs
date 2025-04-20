using Application.DTOs.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IChatService
    {
        Task<ChatDto?> GetChatWithMessagesAsync(int chatId);
        Task<IEnumerable<ChatDto>> GetUserChatsAsync(int userId);
        Task CreateChatAsync(ChatCreateDto chatDto);
        Task DeleteChatAsync(int chatId);
    }
}
