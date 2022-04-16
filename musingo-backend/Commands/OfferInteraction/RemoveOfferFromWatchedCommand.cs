using MediatR;
using musingo_backend.Models;

namespace musingo_backend.Commands;

public class RemoveOfferFromWatchedCommand : IRequest<HandlerResult<Offer>>
{
    public int UserId { get; set; }
    public int OfferId { get; set; }
}