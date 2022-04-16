using MediatR;
using musingo_backend.Models;

namespace musingo_backend.Queries;

public class GetUserCommentsQuery: IRequest<HandlerResult<ICollection<UserComment>>>
{
    public int UserId { get; set; }
}