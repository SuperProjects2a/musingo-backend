using MediatR;
using musingo_backend.Models;
using musingo_backend.Queries;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class GetCommentByIdHandler: IRequestHandler<GetCommentByIdQuery,UserComment?>
{
    private readonly IUserCommentRepository _userCommentRepository;

    public GetCommentByIdHandler(IUserCommentRepository userCommentRepository)
    {
        _userCommentRepository = userCommentRepository;
    }

    public async Task<UserComment?> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _userCommentRepository.GetCommentById(request.CommentId);
        return result;
    }
}