using MediatR;
using musingo_backend.Models;

namespace musingo_backend.Queries;

public class GetCommentByIdQuery : IRequest<HandlerResult<UserComment>>
{
    public int CommentId { get; set; }
}