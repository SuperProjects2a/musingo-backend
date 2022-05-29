using MediatR;
using musingo_backend.Dtos;
namespace musingo_backend.Queries;

public class GetOffersWatchedByUserQuery : IRequest<HandlerResult<ICollection<OfferDetailsDto>>>
{
    public int UserId { get; set; }
}