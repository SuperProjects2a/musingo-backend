using MediatR;
using musingo_backend.Commands;
using musingo_backend.Models;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class UpdateOfferHandler : IRequestHandler<UpdateOfferCommand, Offer?>
{
    private readonly IOfferRepository _offerRepository;

    public UpdateOfferHandler(IOfferRepository offerRepository)
    {
        _offerRepository = offerRepository;
    }

    public async Task<Offer?> Handle(UpdateOfferCommand request, CancellationToken cancellationToken)
    {
        var offer = await _offerRepository.GetOfferById(request.OfferId);
        if (offer is null) return null;

        if (offer.Owner?.Id != request.UserId) return null;

        if (offer.OfferStatus == OfferStatus.Sold || offer.OfferStatus == OfferStatus.Cancelled)
        {
            return null;
        }

        offer.Title = request.Title;
        offer.Cost = request.Cost;
        offer.ImageUrl = request.ImageUrl;
        if (Enum.TryParse<OfferStatus>(request.OfferStatus, out var status)) offer.OfferStatus = status;
        else return null;
        if (Enum.TryParse<ItemCategory>(request.ItemCategory, out var category)) offer.ItemCategory = category;
        else return null;

        offer.Description = request.Description;

        await _offerRepository.UpdateOffer(offer);

        return offer;
    }

}