using Application.DTOs.Notification;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;

    public NotificationService(INotificationRepository notificationRepository, IUnitOfWork uof, IMapper mapper)
    {
        _notificationRepository = notificationRepository;
        _uof = uof;
        _mapper = mapper;
    }

    public async Task<IEnumerable<NotificationDto>> GetUnreadNotificationsAsync(int userId)
    {
        var notifications = await _notificationRepository.GetUnreadNotificationsByUserIdAsync(userId);
        return _mapper.Map<IEnumerable<NotificationDto>>(notifications);
    }

    public async Task MarkAllAsReadAsync(int userId)
    {
        await _notificationRepository.MarkAllAsReadAsync(userId);
    }

    public async Task AddNotificationAsync(NotificationCreateDto notificationDto)
    {
        var notification = _mapper.Map<Notifications>(notificationDto);
        await _notificationRepository.AddAsync(notification);
        await _uof.CompleteAsync();
    }
}