using MediatR;
using musingo_backend.Models;

namespace musingo_backend.Queries;

public class GetCommentByIdQuery : IRequest<UserComment?>
{
    public int CommentId { get; set; }
}