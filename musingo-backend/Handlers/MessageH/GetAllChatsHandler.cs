using AutoMapper;
using MediatR;
using musingo_backend.Dtos;
using musingo_backend.Models;
using musingo_backend.Queries.MessageQ;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers.MessageH;

public class GetAllChatsHandler: IRequestHandler<GetAllChatsQuery,HandlerResult<ICollection<MessageChatDto>>>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IUserRepository _userRepository;
    private readonly IImageUrlRepository _imageUrlRepository;
    private readonly IMapper _mapper;

    public GetAllChatsHandler(IMessageRepository messageRepository, IUserRepository userRepository, IImageUrlRepository imageUrlRepository, IMapper mapper)
    {
        _messageRepository = messageRepository;
        _userRepository = userRepository;
        _imageUrlRepository = imageUrlRepository;
        _mapper = mapper;
    }

    public async Task<HandlerResult<ICollection<MessageChatDto>>> Handle(GetAllChatsQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserById(request.UserId);

        if (user is null) return new HandlerResult<ICollection<MessageChatDto>> { Status = 404 };

        var messages = await _messageRepository.GetLatestMessages(user.Id);

        var messagesDto = _mapper.Map<ICollection<MessageChatDto>>(messages);

        foreach (var message in messagesDto)
        {
            message.UnReadMessagesCount =
                await _messageRepository.UnreadMessageCount(message.Transaction.Id, request.UserId);
            if (message.Transaction.Offer is not null)
                message.Transaction.Offer.ImageUrl =
                    await _imageUrlRepository.GetFirstImageUrlByOfferId(message.Transaction.Offer.Id);
        }

        messagesDto = messagesDto.OrderByDescending(x => x.UnReadMessagesCount).ToList();

        return new HandlerResult<ICollection<MessageChatDto>> { Body = messagesDto, Status = 200 };
    }
}