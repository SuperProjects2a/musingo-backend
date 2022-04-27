using MediatR;
using musingo_backend.Dtos;

namespace musingo_backend.Queries.MessageQ;

public class GetAllChatsQuery: IRequest<HandlerResult<ICollection<MessageChatDto>>>
{
    public int UserId { get; set; }
}