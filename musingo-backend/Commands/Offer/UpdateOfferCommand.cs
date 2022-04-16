using MediatR;
using musingo_backend.Models;

namespace musingo_backend.Commands;

public class UpdateOfferCommand : IRequest<HandlerResult<Offer>>
{
    public int UserId { get; set; }
    public int OfferId { get; set; }
    public string Title { get; set; }
    public string? ImageUrl { get; set; }
    public double Cost { get; set; }
    public string OfferStatus { get; set; }
    public string ItemCategory { get; set; }
    public string Description { get; set; }
}