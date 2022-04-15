using MediatR;
using musingo_backend.Commands;
using musingo_backend.Models;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class AddCommentHandler : IRequestHandler<AddCommentCommand,UserComment?>
{
    private readonly IUserRepository _userRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly ICommentRepository _commentRepository;

    public AddCommentHandler(IUserRepository userRepository, ITransactionRepository transactionRepository, ICommentRepository commentRepository)
    {
        _userRepository = userRepository;
        _transactionRepository = transactionRepository;
        _commentRepository = commentRepository;
    }

    public async Task<UserComment?> Handle(AddCommentCommand request, CancellationToken cancellationToken)
    {
        var transaction = await _transactionRepository.GetTransaction(request.TransactionId);
        if (transaction is null) return null;
        if (transaction.Status != TransactionStatus.Finished)
        {
            return null;
        }
        if (request.UserId != transaction.Buyer.Id && request.UserId != transaction.Seller.Id)
        {
            return null;
        }

        var isCommented = await _commentRepository.IsCommented(transaction.Id, request.UserId);
        if (isCommented is not null)
        {
            return null;
        }

        var comment = new UserComment
        {
            Transaction = transaction,
            CommentText = request.CommentText,
            Rating = request.Rating,
            User = await _userRepository.GetUserById(request.UserId)
        };

        if (comment.User is null)
            return null;

        await _commentRepository.AddComment(comment);

        return comment;
    }
}