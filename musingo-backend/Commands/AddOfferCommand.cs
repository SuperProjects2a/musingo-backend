using MediatR;
using musingo_backend.Models;

namespace musingo_backend.Commands;

public class AddOfferCommand: IRequest<Offer?>
{
    public int UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public double Cost { get; set; }
    public string ItemCategory { get; set; }
}