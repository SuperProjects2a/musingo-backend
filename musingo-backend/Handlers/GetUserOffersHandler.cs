using MediatR;
using musingo_backend.Models;
using musingo_backend.Queries;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class GetUserOffersHandler: IRequestHandler<GetUserOffersQuery,ICollection<Offer>?>
{
    private readonly IOfferRepository _offerRepository;

    public GetUserOffersHandler(IOfferRepository offerRepository)
    {
        _offerRepository = offerRepository;
    }

    public async Task<ICollection<Offer>?> Handle(GetUserOffersQuery request, CancellationToken cancellationToken)
    {
        return await _offerRepository.GetUserOffers(request.UserId);
    }
}