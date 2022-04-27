using MediatR;
using musingo_backend.Models;
using musingo_backend.Queries.MessageQ;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers.MessageH;

public class GetAllChatsHandler: IRequestHandler<GetAllChatsQuery,HandlerResult<ICollection<Message>>>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IUserRepository _userRepository;

    public GetAllChatsHandler(IMessageRepository messageRepository, IUserRepository userRepository)
    {
        _messageRepository = messageRepository;
        _userRepository = userRepository;
    }

    public async Task<HandlerResult<ICollection<Message>>> Handle(GetAllChatsQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserById(request.UserId);

        if (user is null) return new HandlerResult<ICollection<Message>> { Status = 404 };

        var messages = await _messageRepository.GetLatestMessages(user.Id);

        return new HandlerResult<ICollection<Message>> { Body = messages, Status = 200 };
    }
}