using MediatR;
using musingo_backend.Models;

namespace musingo_backend.Commands;

public class ReportOfferCommand: IRequest<HandlerResult<Report>>
{
    public string Reason { get; set; }
    public string? Text { get; set; }
    public int UserId { get; set; }
    public int OfferId { get; set; }
}