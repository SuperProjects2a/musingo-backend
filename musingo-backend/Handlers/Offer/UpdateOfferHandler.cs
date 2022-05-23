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

        var offer = await _offerRepository.GetOfferById(request.Id);
        if (offer is null) return new HandlerResult<Offer>() { Status = 404 };
       
        if(offer.IsBanned) return new HandlerResult<Offer>() { Status = 1 };

        if (offer.Owner?.Id != request.UserId) return new HandlerResult<Offer>() { Status = 403 };


        if (offer.OfferStatus == OfferStatus.Sold || offer.OfferStatus == OfferStatus.Cancelled)
            return new HandlerResult<Offer>() { Status = 403 };


        offer.Title = request.Title;
        offer.Cost = request.Cost;
        if (Enum.TryParse<OfferStatus>(request.OfferStatus, out var status)) offer.OfferStatus = status;
        else return new HandlerResult<Offer>() { Status = 403 };
        
        if (Enum.TryParse<ItemCategory>(request.ItemCategory, out var category)) offer.ItemCategory = category;
        else return new HandlerResult<Offer>() { Status = 403 };


        offer.Description = request.Description;

        await _offerRepository.UpdateOffer(offer);

        return new HandlerResult<Offer>() {Body = offer,Status = 200 };
    }

}