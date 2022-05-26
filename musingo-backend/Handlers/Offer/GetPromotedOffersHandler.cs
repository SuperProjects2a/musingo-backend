using MediatR;
using musingo_backend.Dtos;
using musingo_backend.Models;
using musingo_backend.Queries;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class GetPromotedOffersHandler : IRequestHandler<GetPromotedOffersQuery, HandlerResult<ICollection<Offer>>>
{
    private readonly IOfferRepository _offerRepository;

    public GetPromotedOffersHandler(IOfferRepository offerRepository)
    {
        _offerRepository = offerRepository;
    }
    public async Task<HandlerResult<ICollection<Offer>>> Handle(GetPromotedOffersQuery request, CancellationToken cancellationToken)
    {
        return new HandlerResult<ICollection<Offer>>()
            { Body = await _offerRepository.GetPromotedOffers(), Status = 200 };
    }
}