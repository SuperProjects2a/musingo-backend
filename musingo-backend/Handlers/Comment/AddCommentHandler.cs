using MediatR;
using musingo_backend.Commands;
using musingo_backend.Models;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class AddCommentHandler : IRequestHandler<AddCommentCommand,HandlerResult<UserComment>>
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

    public async Task<HandlerResult<UserComment>> Handle(AddCommentCommand request, CancellationToken cancellationToken)
    {
        var result = new HandlerResult<UserComment>();
        
        var transaction = await _transactionRepository.GetTransaction(request.TransactionId);
        
        if (transaction is null)
        {
            result.Status = 404;
            return result;
        }
        if (transaction.Status != TransactionStatus.Finished)
        {
            result.Status = 1;
            return result;
        }
        if (request.UserId != transaction.Buyer.Id && request.UserId != transaction.Seller.Id)
        {
            result.Status = 2;
            return result;
        }

        var isCommented = await _userCommentRepository.IsCommented(transaction.Id, request.UserId);
        if (isCommented is not null)
        {
            result.Status = 3;
            return result;
        }

        var comment = new UserComment
        {
            Transaction = transaction,
            CommentText = request.CommentText,
            Rating = request.Rating,
            User = await _userRepository.GetUserById(request.UserId)
        };

        if (comment.User is null)
        {
            result.Status = 404;
            return result;
        }

        await _userCommentRepository.AddComment(comment);

        result.Body = comment;
        return result;
    }
}