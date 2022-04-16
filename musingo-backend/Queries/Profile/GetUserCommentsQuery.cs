using MediatR;
using musingo_backend.Models;

namespace musingo_backend.Queries;

public class GetUserCommentsQuery: IRequest<HandlerResultCollection<UserComment>>
{
    public int UserId { get; set; }
}