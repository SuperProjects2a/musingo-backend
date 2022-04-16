using MediatR;
using musingo_backend.Models;
using musingo_backend.Queries;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class GetUserCommentsHandler: IRequestHandler<GetUserCommentsQuery, HandlerResultCollection<UserComment>>
{
    private readonly IUserCommentRepository _userCommentRepository;

    public GetUserCommentsHandler(IUserCommentRepository userCommentRepository)
    {
        _userCommentRepository = userCommentRepository;
    }

    public async Task<HandlerResultCollection<UserComment>> Handle(GetUserCommentsQuery request, CancellationToken cancellationToken)
    {
        var result = new HandlerResultCollection<UserComment>();
        result.Body = await _userCommentRepository.GetUserComments(request.UserId);
        return result;
    }
}