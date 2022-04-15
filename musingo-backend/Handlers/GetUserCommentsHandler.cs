using MediatR;
using musingo_backend.Models;
using musingo_backend.Queries;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class GetUserCommentsHandler: IRequestHandler<GetUserCommentsQuery,ICollection<UserComment>?>
{
    private readonly ICommentRepository _commentRepository;

    public GetUserCommentsHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<ICollection<UserComment>?> Handle(GetUserCommentsQuery request, CancellationToken cancellationToken)
    {
        return await _commentRepository.GetUserComments(request.UserId);
    }
}