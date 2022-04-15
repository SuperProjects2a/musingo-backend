using MediatR;
using musingo_backend.Commands;
using musingo_backend.Models;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class RemoveCommentHandler: IRequestHandler<RemoveCommentCommand,UserComment?>
{
    private readonly IUserCommentRepository _userCommentRepository;

    public RemoveCommentHandler(IUserCommentRepository userCommentRepository)
    {
        _userCommentRepository = userCommentRepository;
    }

    public async Task<UserComment?> Handle(RemoveCommentCommand request, CancellationToken cancellationToken)
    {
        var commentToRemove = await _userCommentRepository.GetCommentById(request.CommentId);

        if (commentToRemove is null) return null;

        if (commentToRemove.User.Id != request.UserId) return null;

        var result = await _userCommentRepository.RemoveComment(commentToRemove);

        return commentToRemove;
    }
}