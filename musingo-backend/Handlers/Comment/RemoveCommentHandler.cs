using MediatR;
using musingo_backend.Commands;
using musingo_backend.Models;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class RemoveCommentHandler: IRequestHandler<RemoveCommentCommand, HandlerResult<UserComment>>
{
    private readonly IUserCommentRepository _userCommentRepository;

    public RemoveCommentHandler(IUserCommentRepository userCommentRepository)
    {
        _userCommentRepository = userCommentRepository;
    }

    public async Task<HandlerResult<UserComment>> Handle(RemoveCommentCommand request, CancellationToken cancellationToken)
    {
        var result = new HandlerResult<UserComment>();
        var commentToRemove = await _userCommentRepository.GetCommentById(request.CommentId);

        if (commentToRemove is null)
        {
            result.Status = 404;
            return result;
        }

        if (commentToRemove.User.Id != request.UserId)
        {
            result.Status = 403;
            return result;
        }
        
        await _userCommentRepository.RemoveComment(commentToRemove);

        result.Body = commentToRemove;

        return result;
    }
}