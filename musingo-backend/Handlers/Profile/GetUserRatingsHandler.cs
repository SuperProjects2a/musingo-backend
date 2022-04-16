using MediatR;
using musingo_backend.Models;
using musingo_backend.Queries;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class GetUserRatingsHandler: IRequestHandler<GetUserRatingsQuery, HandlerResult<ICollection<UserComment>>>
{
    private readonly IUserCommentRepository _userCommentRepository;

    public GetUserRatingsHandler(IUserCommentRepository userCommentRepository)
    {
        _userCommentRepository = userCommentRepository;
    }

    public async Task<HandlerResult<ICollection<UserComment>>> Handle(GetUserRatingsQuery request, CancellationToken cancellationToken)
    {
        var result = new HandlerResult<ICollection<UserComment>>();
        result.Body = await _userCommentRepository.GetUserRatings(request.UserId);
        return result;
    }
}