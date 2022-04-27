using MediatR;
using Microsoft.EntityFrameworkCore;
using musingo_backend.Commands;
using musingo_backend.Models;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class OpenTransactionHandler : IRequestHandler<OpenTransactionCommand, HandlerResult<Transaction>>
{
    private ITransactionRepository _transactionRepository;
    private IUserRepository _userRepository;
    private IOfferRepository _offerRepository;
    private IMessageRepository _messageRepository;

    public OpenTransactionHandler(ITransactionRepository transactionRepository, IUserRepository userRepository, IOfferRepository offerRepository, IMessageRepository messageRepository)
    {
        _transactionRepository = transactionRepository;
        _userRepository = userRepository;
        _offerRepository = offerRepository;
        _messageRepository = messageRepository;
    }

    public async Task<HandlerResult<Transaction>> Handle(OpenTransactionCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserById(request.UserId);
        var offer = await _offerRepository.GetOfferById(request.OfferId);

        if (offer is null) return new HandlerResult<Transaction>() {Status = 404};
        if (offer.OfferStatus != OfferStatus.Active) return new HandlerResult<Transaction>() {Status = 2};
        if (offer.Owner.Id == user.Id) return new HandlerResult<Transaction>() {Status = 403};

        var existingTransaction = await _transactionRepository.GetAllTransactions()
            .Include(x => x.Offer)
            .Include(x => x.Buyer)
            .Where(x => x.Offer.Id == request.OfferId)
            .Where(x => x.Buyer.Id == request.UserId)
            .FirstOrDefaultAsync(cancellationToken);

        if (existingTransaction is not null) return new HandlerResult<Transaction>() {Status = 403};

        var transaction = new Transaction()
        {
            Offer = offer,
            Seller = offer.Owner,
            Buyer = user,
            Status = TransactionStatus.Opened,
            Cost = offer.Cost
        };
        
        var createdTransaction = await _transactionRepository.AddTransaction(transaction);

        var message = new Message()
        {
            Transaction = transaction,
            Sender = user,
            Text = request.Message
        };
        await _messageRepository.SendMessage(message);

        return new HandlerResult<Transaction>() {Body = createdTransaction, Status = 200};
    }
}