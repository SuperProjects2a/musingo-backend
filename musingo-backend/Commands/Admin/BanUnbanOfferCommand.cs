using MediatR;
using musingo_backend.Models;

namespace musingo_backend.Commands.Admin;

public class BanUnbanOfferCommand: IRequest<HandlerResult<Offer>>
{
    public int OfferId { get; set; }
}