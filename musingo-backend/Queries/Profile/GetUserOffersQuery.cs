using MediatR;
using musingo_backend.Models;

namespace musingo_backend.Queries;

public class GetUserOffersQuery : IRequest<HandlerResult<ICollection<Offer>>>
{
    public int UserId { get; set; }
}