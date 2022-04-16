using MediatR;
using musingo_backend.Models;
using musingo_backend.Queries;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class GetUserRatingsHandler: IRequestHandler<GetUserRatingsQuery, HandlerResultCollection<UserComment>>
{
    private readonly IUserCommentRepository _userCommentRepository;

    public GetUserRatingsHandler(IUserCommentRepository userCommentRepository)
    {
        _userCommentRepository = userCommentRepository;
    }

    public async Task<HandlerResultCollection<UserComment>> Handle(GetUserRatingsQuery request, CancellationToken cancellationToken)
    {
        var result = new HandlerResultCollection<UserComment>();
        result.Body = await _userCommentRepository.GetUserRatings(request.UserId);
        return result;
    }
}