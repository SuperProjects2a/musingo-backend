using MediatR;
using musingo_backend.Models;

namespace musingo_backend.Queries;

public class GetOffersWatchedByUserQuery : IRequest<ICollection<Offer>?>
{
    public int UserId { get; set; }
}