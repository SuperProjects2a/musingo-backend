using MediatR;
using musingo_backend.Dtos;

namespace musingo_backend.Queries;

public class GetUserOffersQuery : IRequest<HandlerResult<ICollection<OfferDetailsDto>>>
{
    public int UserId { get; set; }
}