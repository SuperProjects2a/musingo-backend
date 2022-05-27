using musingo_backend.Models;

namespace musingo_backend.Dtos;

public class UserCommentDto
{
    public TransactionDto Transaction { get; set; }
    public double? Rating { get; set; }
    public string? CommentText { get; set; }
    public UserDto User { get; set; }
}