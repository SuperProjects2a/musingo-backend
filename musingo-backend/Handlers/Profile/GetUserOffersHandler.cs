using MediatR;
using musingo_backend.Models;
using musingo_backend.Queries;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class GetUserOffersHandler: IRequestHandler<GetUserOffersQuery, HandlerResultCollection<Offer>>
{
    private readonly IOfferRepository _offerRepository;

    public GetUserOffersHandler(IOfferRepository offerRepository)
    {
        _offerRepository = offerRepository;
    }

    public async Task<HandlerResultCollection<Offer>> Handle(GetUserOffersQuery request, CancellationToken cancellationToken)
    {
        var result = new HandlerResultCollection<Offer>();
        result.Body = await _offerRepository.GetUserOffers(request.UserId);
        return result;
    }
}