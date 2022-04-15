using MediatR;
using musingo_backend.Models;

namespace musingo_backend.Commands;

public class AddOfferToWatchedCommand : IRequest<Offer?>
{
    public int OfferId { get; set; }
    public int UserId { get; set; }
}