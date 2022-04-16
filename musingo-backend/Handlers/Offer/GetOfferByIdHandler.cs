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
        var result = new HandlerResult<Offer>();
        var offer = await _offerRepository.GetOfferById(request.Id);

        if (offer is null)
        {
            result.Status = 404;
            return result;
        }

        result.Body = offer;

        return result;
    }
}