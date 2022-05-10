using MediatR;
using musingo_backend.Models;
using musingo_backend.Queries;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class GetOfferByIdHandler : IRequestHandler<GetOfferByIdQuery, HandlerResult<Offer>>
{
    private readonly IOfferRepository _offerRepository;

    public GetOfferByIdHandler(IOfferRepository offerRepository)
    {
        _offerRepository = offerRepository;
    }

    public async Task<HandlerResult<Offer>> Handle(GetOfferByIdQuery request, CancellationToken cancellationToken)
    {
        var offer = await _offerRepository.GetOfferById(request.Id);

        if (offer is null) return new HandlerResult<Offer>() { Status = 404 };

        if (offer.IsBanned) return new HandlerResult<Offer>() { Status = 1 };

        return new HandlerResult<Offer>() { Body = offer, Status = 200 };
    }
}