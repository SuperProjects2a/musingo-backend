using MediatR;
using musingo_backend.Commands;
using musingo_backend.Models;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class UpdateCommentHandler: IRequestHandler<UpdateCommentCommand,UserComment?>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserCommentRepository _userCommentRepository;

    public UpdateCommentHandler(IUserRepository userRepository, IUserCommentRepository userCommentRepository)
    {
        _userRepository = userRepository;
        _userCommentRepository = userCommentRepository;
    }

    public async Task<UserComment?> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {
        var userComment = await _userCommentRepository.GetCommentById(request.CommentId);

        if (userComment is null) return null;

        if (userComment.User.Id != request.UserId) return null;

        if (request.Rating is not null) userComment.Rating = (double) request.Rating;

        if (!String.IsNullOrEmpty(request.CommentText)) userComment.CommentText = request.CommentText;

         await _userCommentRepository.UpdateComment(userComment);

         return userComment;
    }
}