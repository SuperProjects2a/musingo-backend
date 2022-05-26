using MediatR;
using musingo_backend.Dtos;
using musingo_backend.Models;

namespace musingo_backend.Queries;

public class GetPromotedOffersQuery : IRequest<HandlerResult<ICollection<OfferDetailsDto>>>
{
    
}