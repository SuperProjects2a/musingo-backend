using MediatR;
using musingo_backend.Commands;
using musingo_backend.Models;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class PurchaseHandler : IRequestHandler<PurchaseCommand, HandlerResult<Transaction>>
{
    private IOfferRepository _offerRepository;
    private IUserRepository _userRepository;
    private ITransactionRepository _transactionRepository;

    public PurchaseHandler(IOfferRepository offerRepository, IUserRepository userRepository, ITransactionRepository transactionRepository)
    {
        _offerRepository = offerRepository;
        _userRepository = userRepository;
        _transactionRepository = transactionRepository;
    }

    public async Task<HandlerResult<Transaction>> Handle(PurchaseCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserById(request.UserId);
        var offer = await _offerRepository.GetOfferById(request.OfferId);
        
        if (offer is null) return new HandlerResult<Transaction>() {Status = 404};
        if (offer.Cost > user.WalletBalance) return new HandlerResult<Transaction>() {Status = 1};
        if (offer.OfferStatus != OfferStatus.Active) return new HandlerResult<Transaction>() {Status = 2};
        if (offer.Owner.Id == user.Id) return new HandlerResult<Transaction>() {Status = 403};

        var transaction = new Transaction()
        {
            Offer = offer,
            Seller = offer.Owner,
            Buyer = user,
            Status = TransactionStatus.Finished,
            Cost = offer.Cost
        };
        
        var seller = transaction.Seller;

        user.WalletBalance -= offer.Cost;
        seller.WalletBalance += transaction.Cost;

        await _userRepository.UpdateUser(user);
        await _userRepository.UpdateUser(seller);

        offer.OfferStatus = OfferStatus.Sold;
        await _offerRepository.UpdateOffer(offer);
        
        var createdTransaction = await _transactionRepository.AddTransaction(transaction);
        return new HandlerResult<Transaction>() {Body = createdTransaction, Status = 200};
    }
}