using MediatR;
using musingo_backend.Models;

namespace musingo_backend.Commands;

public class RemoveCommentCommand:IRequest<HandlerResult<UserComment>>
{
    public int UserId { get; set; }
    public int CommentId { get; set; }
}