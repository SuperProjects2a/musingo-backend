using MediatR;
using musingo_backend.Commands;
using musingo_backend.Models;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class UpdateTransactionHandler : IRequestHandler<UpdateTransactionCommand, HandlerResult<Transaction>>
{
    private ITransactionRepository _transactionRepository;
    private IUserRepository _userRepository;

    public UpdateTransactionHandler(ITransactionRepository transactionRepository, IUserRepository userRepository)
    {
        _transactionRepository = transactionRepository;
        _userRepository = userRepository;
    }

    public async Task<HandlerResult<Transaction>> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
    {
        var transaction = await _transactionRepository.GetTransaction(request.TransactionId);

        if (transaction is null) return new HandlerResult<Transaction>() {Status = 404};
        if (transaction.Seller.Id != request.UserId) return new HandlerResult<Transaction>() {Status = 403};
        if (transaction.Status == TransactionStatus.Finished || transaction.Status == TransactionStatus.Declined)
        {
            return new HandlerResult<Transaction>() { Status = 3 };
        }

        transaction.Cost = request.Cost;
        transaction.Status = request.TransactionStatus;

        var result = await _transactionRepository.UpdateTransaction(transaction);
        return new HandlerResult<Transaction>() {Body = result, Status = 200};
    }
}