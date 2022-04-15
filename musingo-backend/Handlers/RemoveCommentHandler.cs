using MediatR;
using musingo_backend.Commands;
using musingo_backend.Models;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class RemoveCommentHandler: IRequestHandler<RemoveCommentCommand,UserComment?>
{
    private readonly ICommentRepository _commentRepository;

    public RemoveCommentHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<UserComment?> Handle(RemoveCommentCommand request, CancellationToken cancellationToken)
    {
        var commentToRemove = await _commentRepository.GetCommentById(request.CommentId);

        if (commentToRemove is null) return null;

        if (commentToRemove.User.Id != request.UserId) return null;

        var result = await _commentRepository.RemoveComment(commentToRemove);

        return commentToRemove;
    }
}