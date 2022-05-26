using MediatR;
using musingo_backend.Models;

namespace musingo_backend.Queries;

public class GetPromotedOffersQuery : IRequest<HandlerResult<ICollection<Offer>>>
{
    
}