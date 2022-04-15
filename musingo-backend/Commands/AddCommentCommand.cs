using MediatR;
using musingo_backend.Models;

namespace musingo_backend.Commands;

public class AddCommentCommand : IRequest<UserComment?>
{
    public int TransactionId { get; set; }
    public int UserId { get; set; }
    public double Rating { get; set; }
    public string? CommentText { get; set; }
}