using MediatR;
using musingo_backend.Models;

namespace musingo_backend.Queries;

public class GetOffersWatchedByUserQuery : IRequest<HandlerResultCollection<Offer>>
{
    public int UserId { get; set; }
}