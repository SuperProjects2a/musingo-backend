using MediatR;
using musingo_backend.Commands;
using musingo_backend.Models;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class UpdateCommentHandler : IRequestHandler<UpdateCommentCommand, HandlerResult<UserComment>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserCommentRepository _userCommentRepository;

    public UpdateCommentHandler(IUserRepository userRepository, IUserCommentRepository userCommentRepository)
    {
        _userRepository = userRepository;
        _userCommentRepository = userCommentRepository;
    }

    public async Task<HandlerResult<UserComment>> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {
        var result = new HandlerResult<UserComment>();

        var userComment = await _userCommentRepository.GetCommentById(request.Id);

        if (userComment is null)
        {
            result.Status = 404;
            return result;
        }

        if (userComment.User.Id != request.UserId)
        {
            result.Status = 403;
            return result;
        }

        if (request.Rating is not null) userComment.Rating = (double)request.Rating;

        if (!String.IsNullOrEmpty(request.CommentText)) userComment.CommentText = request.CommentText;

        await _userCommentRepository.UpdateComment(userComment);

        result.Body = userComment;
        return result;
    }
}