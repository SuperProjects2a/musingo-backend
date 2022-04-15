using MediatR;
using musingo_backend.Models;

namespace musingo_backend.Commands;

public class UpdateCommentCommand: IRequest<UserComment?>
{
    public int CommentId { get; set; }
    public int UserId { get; set; }
    public double? Rating { get; set; }
    public string? CommentText { get; set; }
}