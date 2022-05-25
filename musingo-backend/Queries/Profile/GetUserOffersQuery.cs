using MediatR;
using musingo_backend.Dtos;

namespace musingo_backend.Queries;

public class GetUserOffersQuery : IRequest<HandlerResult<ICollection<OfferDto>>>
{
    public int UserId { get; set; }
}