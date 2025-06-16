using Application.DTOs.Chat;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

public class MessageService : IMessageService
{
    private readonly IMessageRepository _messageRepository;
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;

    public MessageService(IMessageRepository messageRepository, IUnitOfWork uof, IMapper mapper)
    {
        _messageRepository = messageRepository;
        _uof = uof;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MessageDto>> GetMessagesByChatIdAsync(int chatId)
    {
        var messages = await _messageRepository.GetMessagesByChatIdAsync(chatId);
        return _mapper.Map<IEnumerable<MessageDto>>(messages);
    }

    public async Task<IEnumerable<MessageDto>> GetRecentMessagesAsync(int chatId, int count)
    {
        var messages = await _messageRepository.GetRecentMessagesAsync(chatId, count);
        return _mapper.Map<IEnumerable<MessageDto>>(messages);
    }

    public async Task AddMessageAsync(MessageCreateDto messageDto)
    {
        var message = _mapper.Map<Messages>(messageDto);
        await _messageRepository.AddAsync(message);
        await _uof.CompleteAsync();
    }
}