using Application.DTOs.Chat;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Aliexpress_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IChatService _chatService;

        public MessagesController(IMessageService messageService, IChatService chatService)
        {
            _messageService = messageService;
            _chatService = chatService;
        }

        [HttpGet("chat/{chatId}")]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetChatMessages(int chatId)
        {
            var currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            // Проверка, что чат существует и пользователь имеет к нему доступ
            var chat = await _chatService.GetChatWithMessagesAsync(chatId);
            if (chat == null)
                return NotFound();

            if (chat.BuyerID != currentUserId && chat.SellerID != currentUserId && !User.IsInRole("Admin"))
                return Forbid();

            var messages = await _messageService.GetMessagesByChatIdAsync(chatId);
            return Ok(messages);
        }

        [HttpGet("chat/{chatId}/recent/{count}")]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetRecentChatMessages(int chatId, int count)
        {
            var currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            // Проверка, что чат существует и пользователь имеет к нему доступ
            var chat = await _chatService.GetChatWithMessagesAsync(chatId);
            if (chat == null)
                return NotFound();

            if (chat.BuyerID != currentUserId && chat.SellerID != currentUserId && !User.IsInRole("Admin"))
                return Forbid();

            var messages = await _messageService.GetRecentMessagesAsync(chatId, count);
            return Ok(messages);
        }

        [HttpPost]
        public async Task<ActionResult> SendMessage([FromBody] MessageCreateDto messageDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            // Проверка, что чат существует и пользователь имеет к нему доступ
            var chat = await _chatService.GetChatWithMessagesAsync(messageDto.ChatID);
            if (chat == null)
                return NotFound();

            if (chat.BuyerID != currentUserId && chat.SellerID != currentUserId)
                return Forbid();

            // Проверка, что отправитель - текущий пользователь
            if (messageDto.SenderId != currentUserId)
                return BadRequest(new { message = "SenderId must be the current user's ID" });

            await _messageService.AddMessageAsync(messageDto);
            return StatusCode(201);
        }
    }
}
