using MediatR;
using musingo_backend.Commands;
using musingo_backend.Models;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class PurchaseFromTransactionHandler : IRequestHandler<PurchaseFromTransactionCommand, HandlerResult<Transaction>>
{
    private ITransactionRepository _transactionRepository;
    private IUserRepository _userRepository;

    public PurchaseFromTransactionHandler(ITransactionRepository transactionRepository, IUserRepository userRepository)
    {
        _transactionRepository = transactionRepository;
        _userRepository = userRepository;
    }

    public async Task<HandlerResult<Transaction>> Handle(PurchaseFromTransactionCommand request, CancellationToken cancellationToken)
    {
        var transaction = await _transactionRepository.GetTransaction(request.TransactionId);
        if (transaction is null) return new HandlerResult<Transaction>() {Status = 404};
        if (transaction.Buyer.Id != request.UserId) return new HandlerResult<Transaction>() {Status = 403};

        var user = await _userRepository.GetUserById(request.UserId);
        if (user.WalletBalance < transaction.Cost) return new HandlerResult<Transaction>() {Status = 1};
        if (transaction.Status != TransactionStatus.Opened) return new HandlerResult<Transaction>() {Status = 2};

        transaction.Status = TransactionStatus.Finished;
        user.WalletBalance -= transaction.Cost;
        
        var seller = transaction.Seller;
        seller.WalletBalance += transaction.Cost;

        await _userRepository.UpdateUser(user);
        await _userRepository.UpdateUser(seller);
        
        var result = await _transactionRepository.UpdateTransaction(transaction);

        return new HandlerResult<Transaction>() {Body = result, Status = 200};

    }
}