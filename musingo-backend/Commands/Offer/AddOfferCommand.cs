using MediatR;
using musingo_backend.Dtos;

namespace musingo_backend.Commands;

public class AddOfferCommand: IRequest<HandlerResult<OfferDetailsDto>>
{
    public int UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public double Cost { get; set; }
    public string ItemCategory { get; set; }
    public ICollection<string> ImageUrls { get; set; }
    public string Email { get; set; }
    public string? City { get; set; }
    public string? PhoneNumber { get; set; }
}