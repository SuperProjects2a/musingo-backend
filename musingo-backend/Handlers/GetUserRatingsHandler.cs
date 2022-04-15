using MediatR;
using musingo_backend.Models;
using musingo_backend.Queries;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class GetUserRatingsHandler: IRequestHandler<GetUserRatingsQuery,ICollection<UserComment>?>
{
    private readonly ICommentRepository _commentRepository;

    public GetUserRatingsHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<ICollection<UserComment>?> Handle(GetUserRatingsQuery request, CancellationToken cancellationToken)
    {
        return await _commentRepository.GetUserRatings(request.UserId);
    }
}