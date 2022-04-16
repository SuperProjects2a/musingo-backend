using MediatR;
using musingo_backend.Commands;
using musingo_backend.Models;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class UpdateOfferHandler : IRequestHandler<UpdateOfferCommand, HandlerResult<Offer>>
{
    private readonly IOfferRepository _offerRepository;

    public UpdateOfferHandler(IOfferRepository offerRepository)
    {
        _offerRepository = offerRepository;
    }

    public async Task<HandlerResult<Offer>> Handle(UpdateOfferCommand request, CancellationToken cancellationToken)
    {
        var result = new HandlerResult<Offer>();

        var offer = await _offerRepository.GetOfferById(request.OfferId);
        if (offer is null)
        {
            result.Status = 404;
            return result;
        }

        if (offer.Owner?.Id != request.UserId)
        {
            result.Status = 403;
            return result;
        }

        if (offer.OfferStatus == OfferStatus.Sold || offer.OfferStatus == OfferStatus.Cancelled)
        {
            result.Status = 403;
            return result;
        }


        offer.Title = request.Title;
        offer.Cost = request.Cost;
        offer.ImageUrl = request.ImageUrl;
        if (Enum.TryParse<OfferStatus>(request.OfferStatus, out var status)) offer.OfferStatus = status;
        else 
        {
            result.Status = 403;
            return result;
        }
        if (Enum.TryParse<ItemCategory>(request.ItemCategory, out var category)) offer.ItemCategory = category;
        else
        {
            result.Status = 403;
            return result;
        };

        offer.Description = request.Description;

        await _offerRepository.UpdateOffer(offer);

        result.Body = offer;

        return result;
    }

}