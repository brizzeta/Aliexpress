using Application.DTOs.Chat;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

public class ChatService : IChatService
{
    private readonly IChatRepository _chatRepository;
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;

    public ChatService(IChatRepository chatRepository, IUnitOfWork uof, IMapper mapper)
    {
        _chatRepository = chatRepository;
        _uof = uof;
        _mapper = mapper;
    }

    public async Task<ChatDto?> GetChatWithMessagesAsync(int chatId)
    {
        var chat = await _chatRepository.GetChatWithMessagesAsync(chatId);
        return _mapper.Map<ChatDto>(chat);
    }

    public async Task<IEnumerable<ChatDto>> GetUserChatsAsync(int userId)
    {
        var chats = await _chatRepository.GetChatsByUserIdAsync(userId);
        return _mapper.Map<IEnumerable<ChatDto>>(chats);
    }

    public async Task CreateChatAsync(ChatCreateDto chatDto)
    {
        var chat = _mapper.Map<Chat>(chatDto);
        await _chatRepository.AddAsync(chat);
        await _uof.CompleteAsync();
    }

    public async Task DeleteChatAsync(int chatId)
    {
        var chat = await _chatRepository.GetByIdAsync(chatId);
        if (chat != null)
        {
            _chatRepository.Delete(chat);
            await _uof.CompleteAsync();
        }
    }
}