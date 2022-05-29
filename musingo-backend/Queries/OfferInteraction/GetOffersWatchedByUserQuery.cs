using MediatR;
using musingo_backend.Dtos;
namespace musingo_backend.Queries;

public class GetOffersWatchedByUserQuery : IRequest<HandlerResult<ICollection<OfferDto>>>
{
    public int UserId { get; set; }
}