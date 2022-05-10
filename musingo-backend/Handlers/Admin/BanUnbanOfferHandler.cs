using MediatR;
using musingo_backend.Commands.Admin;
using musingo_backend.Models;
using musingo_backend.Repositories;

namespace musingo_backend.Admin;

public class BanUnbanOfferHandler:IRequestHandler<BanUnbanOfferCommand,HandlerResult<Offer>>
{
    private readonly IOfferRepository _offerRepository;

    public BanUnbanOfferHandler(IOfferRepository offerRepository)
    {
        _offerRepository = offerRepository;
    }


    public async Task<HandlerResult<Offer>> Handle(BanUnbanOfferCommand request, CancellationToken cancellationToken)
    {
        var offer = await _offerRepository.GetOfferById(request.OfferId);

        if (offer is null) return new HandlerResult<Offer> { Status = 404 };

        offer.IsBanned = !offer.IsBanned;

        await _offerRepository.UpdateOffer(offer);

        return new HandlerResult<Offer> { Body = offer, Status = 200 };
    }
}