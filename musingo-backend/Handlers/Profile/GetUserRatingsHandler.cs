using MediatR;
using musingo_backend.Models;
using musingo_backend.Queries;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class GetUserRatingsHandler: IRequestHandler<GetUserRatingsQuery,ICollection<UserComment>?>
{
    private readonly IUserCommentRepository _userCommentRepository;

    public GetUserRatingsHandler(IUserCommentRepository userCommentRepository)
    {
        _userCommentRepository = userCommentRepository;
    }

    public async Task<ICollection<UserComment>?> Handle(GetUserRatingsQuery request, CancellationToken cancellationToken)
    {
        return await _userCommentRepository.GetUserRatings(request.UserId);
    }
}