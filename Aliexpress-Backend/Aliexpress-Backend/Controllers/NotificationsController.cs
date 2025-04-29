using Application.DTOs.Notification;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Aliexpress_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("unread")]
        public async Task<ActionResult<IEnumerable<NotificationDto>>> GetCurrentUserUnreadNotifications()
        {
            var currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var notifications = await _notificationService.GetUnreadNotificationsAsync(currentUserId);
            return Ok(notifications);
        }

        [HttpGet("unread/{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<NotificationDto>>> GetUserUnreadNotifications(int userId)
        {
            var notifications = await _notificationService.GetUnreadNotificationsAsync(userId);
            return Ok(notifications);
        }

        [HttpPost("markAsRead")]
        public async Task<ActionResult> MarkCurrentUserNotificationsAsRead()
        {
            var currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _notificationService.MarkAllAsReadAsync(currentUserId);
            return Ok();
        }

        [HttpPost("markAsRead/{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> MarkUserNotificationsAsRead(int userId)
        {
            await _notificationService.MarkAllAsReadAsync(userId);
            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult> AddNotification([FromBody] NotificationCreateDto notificationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _notificationService.AddNotificationAsync(notificationDto);
            return StatusCode(201);
        }
    }
}
