using MediatR;
using musingo_backend.Models;
using musingo_backend.Queries;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class GetCommentByIdHandler: IRequestHandler<GetCommentByIdQuery, HandlerResult<UserComment>>
{
    private readonly IUserCommentRepository _userCommentRepository;

    public GetCommentByIdHandler(IUserCommentRepository userCommentRepository)
    {
        _userCommentRepository = userCommentRepository;
    }

    public async Task<HandlerResult<UserComment>> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
    {
        var result = new HandlerResult<UserComment>();
        var comment = await _userCommentRepository.GetCommentById(request.CommentId);

        if (comment is null)
        {
            result.Status = 404;
            return result;
        }
        result.Body=comment;
        return result;
    }
}