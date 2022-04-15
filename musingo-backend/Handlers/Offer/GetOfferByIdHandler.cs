using MediatR;
using musingo_backend.Models;
using musingo_backend.Queries;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class GetOfferByIdHandler: IRequestHandler<GetOfferByIdQuery,Offer?>
{
    private readonly IOfferRepository _offerRepository;

    public GetOfferByIdHandler(IOfferRepository offerRepository)
    {
        _offerRepository = offerRepository;
    }

    public async Task<Offer> Handle(GetOfferByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _offerRepository.GetOfferById(request.Id);

        return result;
    }
}