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
    public class ChatsController : ControllerBase
    {
        private readonly IChatService _chatService;
        private readonly IUserService _userService;

        public ChatsController(IChatService chatService, IUserService userService)
        {
            _chatService = chatService;
            _userService = userService;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ChatDto>> GetChat(int id)
        {
            var currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var chat = await _chatService.GetChatWithMessagesAsync(id);

            if (chat == null)
                return NotFound();

            // Проверка доступа пользователя к чату
            if (chat.BuyerID != currentUserId && chat.SellerID != currentUserId)
                return Forbid();

            return Ok(chat);
        }

        [HttpGet("user")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ChatDto>>> GetCurrentUserChats()
        {
            var currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var chats = await _chatService.GetUserChatsAsync(currentUserId);
            return Ok(chats);
        }

        [HttpGet("buyer/{buyerId}")]
        [Authorize(Roles ="Buyer")]
        public async Task<ActionResult<IEnumerable<ChatDto>>> GetBuyerChats(int buyerId)
        {
            var currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            // Проверяем, является ли текущий пользователь запрашиваемым покупателем или администратором
            if (currentUserId != buyerId && !User.IsInRole("Admin"))
                return Forbid();

            var chats = await _chatService.GetUserChatsAsync(buyerId);
            return Ok(chats);
        }

        [HttpGet("seller/{sellerId}")]
        [Authorize(Roles = "Seller")]
        public async Task<ActionResult<IEnumerable<ChatDto>>> GetSellerChats(int sellerId)
        {
            var currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            // Проверяем, является ли текущий пользователь запрашиваемым продавцом или администратором
            if (currentUserId != sellerId && !User.IsInRole("Admin"))
                return Forbid();

            var chats = await _chatService.GetUserChatsAsync(sellerId);
            return Ok(chats);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> CreateChat([FromBody] ChatCreateDto chatDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            // Проверяем, что текущий пользователь является либо покупателем, либо продавцом в этом чате
            if (chatDto.BuyerID != currentUserId && chatDto.SellerID != currentUserId)
                return Forbid();

            await _chatService.CreateChatAsync(chatDto);
            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteChat(int id)
        {
            var currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var chat = await _chatService.GetChatWithMessagesAsync(id);

            if (chat == null)
                return NotFound();

            // Проверка доступа пользователя к чату
            if (chat.BuyerID != currentUserId && chat.SellerID != currentUserId && !User.IsInRole("Admin"))
                return Forbid();

            await _chatService.DeleteChatAsync(id);
            return NoContent();
        }
    }
}
