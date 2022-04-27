using MediatR;
using musingo_backend.Models;

namespace musingo_backend.Queries.MessageQ;

public class GetAllChatsQuery: IRequest<HandlerResult<ICollection<Message>>>
{
    public int UserId { get; set; }
}