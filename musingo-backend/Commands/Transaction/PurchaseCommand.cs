using MediatR;
using musingo_backend.Models;

namespace musingo_backend.Commands;

public class PurchaseCommand : IRequest<HandlerResult<Transaction>>
{
    public int UserId { get; set; }
    public int OfferId { get; set; }
}