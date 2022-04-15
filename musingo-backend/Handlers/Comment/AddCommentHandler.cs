using MediatR;
using musingo_backend.Commands;
using musingo_backend.Models;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class AddCommentHandler : IRequestHandler<AddCommentCommand,UserComment?>
{
    private readonly IUserRepository _userRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IUserCommentRepository _userCommentRepository;

    public AddCommentHandler(IUserRepository userRepository, ITransactionRepository transactionRepository, IUserCommentRepository userCommentRepository)
    {
        _userRepository = userRepository;
        _transactionRepository = transactionRepository;
        _userCommentRepository = userCommentRepository;
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

        var isCommented = await _userCommentRepository.IsCommented(transaction.Id, request.UserId);
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

        await _userCommentRepository.AddComment(comment);

        return comment;
    }
}