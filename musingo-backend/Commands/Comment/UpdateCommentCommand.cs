using MediatR;
using musingo_backend.Models;

namespace musingo_backend.Commands;

public class UpdateCommentCommand: IRequest<HandlerResult<UserComment>>
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public double? Rating { get; set; }
    public string? CommentText { get; set; }
}