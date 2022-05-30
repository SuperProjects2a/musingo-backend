using MediatR;
using musingo_backend.Commands;
using musingo_backend.Models;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class PromoteOfferHandler:IRequestHandler<PromoteOfferCommand, HandlerResult<Offer>>
{
    private readonly IOfferRepository _offerRepository;
    private readonly IUserRepository _userRepository;

    public PromoteOfferHandler(IOfferRepository offerRepository, IUserRepository userRepository)
    {
        _offerRepository = offerRepository;
        _userRepository = userRepository;
    }
    public async Task<HandlerResult<Offer>> Handle(PromoteOfferCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserById(request.UserId);
        if (user is null)
            return new HandlerResult<Offer>() { Status = 404 };

        var offer = await _offerRepository.GetOfferById(request.OfferId);
        if (offer is null)
            return new HandlerResult<Offer>() { Status = 404 };

        if (offer.Owner?.Id != user.Id)
            return new HandlerResult<Offer>() { Status = 403 };

        if (offer.isPromoted)
            return new HandlerResult<Offer>() { Status = 1 };

        if(user.WalletBalance < 10 )
            return new HandlerResult<Offer>() { Status = 2 };

        if(offer.OfferStatus != OfferStatus.Active)
            return new HandlerResult<Offer>() { Status = 3 };

        offer.isPromoted = true;
        user.WalletBalance -= 10;
        await _userRepository.UpdateUser(user);
        await _offerRepository.UpdateOffer(offer);

        return new HandlerResult<Offer>() { Body = offer, Status = 200 };


    }
}