using System.Collections;
using MediatR;
using musingo_backend.Dtos;

namespace musingo_backend.Queries;

public class GetUserOtherOffersQuery: IRequest<HandlerResult<ICollection<OfferDetailsDto>>>
{
    public string Email { get; set; }
    public int OfferId { get; set; }
}