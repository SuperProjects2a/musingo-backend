using MediatR;
using musingo_backend.Models;

namespace musingo_backend.Queries;

public class GetUserOffersQuery : IRequest<HandlerResultCollection<Offer>>
{
    public int UserId { get; set; }
}