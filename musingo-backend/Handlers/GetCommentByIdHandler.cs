using MediatR;
using musingo_backend.Models;
using musingo_backend.Queries;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class GetCommentByIdHandler: IRequestHandler<GetCommentByIdQuery,UserComment?>
{
    private readonly ICommentRepository _commentRepository;

    public GetCommentByIdHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<UserComment?> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _commentRepository.GetCommentById(request.CommentId);
        return result;
    }
}