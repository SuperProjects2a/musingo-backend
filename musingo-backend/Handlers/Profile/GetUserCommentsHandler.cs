using MediatR;
using musingo_backend.Models;
using musingo_backend.Queries;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class GetUserCommentsHandler: IRequestHandler<GetUserCommentsQuery,ICollection<UserComment>?>
{
    private readonly IUserCommentRepository _userCommentRepository;

    public GetUserCommentsHandler(IUserCommentRepository userCommentRepository)
    {
        _userCommentRepository = userCommentRepository;
    }

    public async Task<ICollection<UserComment>?> Handle(GetUserCommentsQuery request, CancellationToken cancellationToken)
    {
        return await _userCommentRepository.GetUserComments(request.UserId);
    }
}